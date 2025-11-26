const app = {
    state: {
        currentCustomer: null,
        currentReservation: null,
    },

    init: function() {
        this.loadPage('enrollment');
    },

    loadPage: function(pageName) {
        const content = document.getElementById('main-content');
        const title = document.getElementById('page-title');
        content.innerHTML = '<div class="text-center"><div class="spinner-border" role="status"></div></div>';

        switch(pageName) {
            case 'enrollment':
                title.innerText = 'التسجيل والحجز';
                this.renderEnrollment();
                break;
            case 'sessions':
                title.innerText = 'إدارة الجلسات';
                this.renderSessions();
                break;
            case 'schools':
                title.innerText = 'إدارة المدارس';
                this.renderSchools();
                break;
            case 'licenses':
                title.innerText = 'أنواع الرخص';
                this.renderLicenses();
                break;
            default:
                content.innerHTML = '<h1>Page not found</h1>';
        }
    },

    // ==========================================
    // Enrollment Logic
    // ==========================================
    renderEnrollment: function() {
        const content = document.getElementById('main-content');
        content.innerHTML = `
            <div class="card mb-4">
                <div class="card-header">بحث عن عميل</div>
                <div class="card-body">
                    <div class="row g-3">
                        <div class="col-md-4">
                            <input type="text" id="search-phone" class="form-control" placeholder="رقم الهاتف">
                        </div>
                        <div class="col-md-4">
                            <input type="text" id="search-national-id" class="form-control" placeholder="الرقم القومي">
                        </div>
                        <div class="col-md-4">
                            <button class="btn btn-primary w-100" onclick="app.searchCustomer()">بحث</button>
                        </div>
                    </div>
                </div>
            </div>
            <div id="customer-result"></div>
        `;
    },

    searchCustomer: async function() {
        const phone = document.getElementById('search-phone').value;
        const nationalId = document.getElementById('search-national-id').value;
        const resultDiv = document.getElementById('customer-result');

        if (!phone && !nationalId) {
            alert('الرجاء إدخال رقم الهاتف أو الرقم القومي');
            return;
        }

        try {
            const query = new URLSearchParams({ phone, nationalId }).toString();
            const response = await fetch(`/api/enrollment/search-customer?${query}`);
            const data = await response.json();

            if (!response.ok || !data.success) {
                // Handle specific case: Customer exists but no payments, or not found
                resultDiv.innerHTML = `<div class="alert alert-warning">${data.message || 'لم يتم العثور على بيانات'}</div>`;
                
                if (data.data) {
                     // Customer found but maybe no payments
                     this.renderCustomerInfo(data.data, false);
                }
                return;
            }

            this.state.currentCustomer = data.data;
            this.renderCustomerInfo(data.data, true);

        } catch (error) {
            console.error(error);
            resultDiv.innerHTML = `<div class="alert alert-danger">حدث خطأ أثناء البحث</div>`;
        }
    },

    renderCustomerInfo: function(customerData, allowBooking) {
        const resultDiv = document.getElementById('customer-result');
        const paymentsHtml = customerData.payments && customerData.payments.length > 0 
            ? customerData.payments.map(p => `
                <div class="list-group-item d-flex justify-content-between align-items-center">
                    <div>
                        <strong>${p.licenseName}</strong> - ${p.amount} EGP <br>
                        <small class="text-muted">${new Date(p.paymentDate).toLocaleDateString()}</small>
                    </div>
                    ${!p.hasReservation ? `<button class="btn btn-sm btn-success" onclick="app.openReservationModal(${p.paymentId}, ${p.licenseId})">حجز الآن</button>` : '<span class="badge bg-secondary">محجوز</span>'}
                </div>
            `).join('')
            : '<div class="list-group-item">لا توجد مدفوعات</div>';

        resultDiv.innerHTML = `
            <div class="card">
                <div class="card-body">
                    <h5 class="card-title">${customerData.fullName}</h5>
                    <p class="card-text">
                        الهاتف: ${customerData.phone} <br>
                        الرقم القومي: ${customerData.nationalId || '-'}
                    </p>
                    <h6>المدفوعات:</h6>
                    <div class="list-group">
                        ${paymentsHtml}
                    </div>
                </div>
            </div>
        `;
    },

    openReservationModal: async function(paymentId, licenseId) {
        // Fetch schools
        const schoolsResp = await fetch('/api/schools'); // Assuming this exists
        const schoolsData = await schoolsResp.json();
        const schools = schoolsData.data || [];

        const modalHtml = `
            <div class="modal fade" id="reservationModal" tabindex="-1">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">إنشاء حجز جديد</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                        </div>
                        <div class="modal-body">
                            <form id="reservation-form">
                                <input type="hidden" id="res-customer-id" value="${this.state.currentCustomer.customerId}">
                                <input type="hidden" id="res-license-id" value="${licenseId}">
                                
                                <div class="mb-3">
                                    <label class="form-label">المدرسة</label>
                                    <select class="form-select" id="res-school-id" required>
                                        ${schools.map(s => `<option value="${s.schoolId}">${s.schoolName}</option>`).join('')}
                                    </select>
                                </div>
                                <div class="mb-3">
                                    <label class="form-label">تاريخ الحجز</label>
                                    <input type="date" class="form-control" id="res-date" value="${new Date().toISOString().split('T')[0]}" required>
                                </div>
                                <div class="mb-3">
                                    <label class="form-label">ملاحظات</label>
                                    <textarea class="form-control" id="res-notes"></textarea>
                                </div>
                            </form>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">إلغاء</button>
                            <button type="button" class="btn btn-primary" onclick="app.submitReservation()">حفظ الحجز</button>
                        </div>
                    </div>
                </div>
            </div>
        `;

        document.getElementById('modal-container').innerHTML = modalHtml;
        const modal = new bootstrap.Modal(document.getElementById('reservationModal'));
        modal.show();
    },

    submitReservation: async function() {
        const customerId = document.getElementById('res-customer-id').value;
        const licenseId = document.getElementById('res-license-id').value;
        const schoolId = document.getElementById('res-school-id').value;
        const notes = document.getElementById('res-notes').value;
        
        const payload = {
            customerId: parseInt(customerId),
            licenseId: parseInt(licenseId),
            schoolId: parseInt(schoolId),
            reservationDate: new Date().toISOString(),
            status: 0, // Assuming 0 is Pending
            notes: notes
        };

        try {
            const response = await fetch('/api/reservations', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(payload)
            });
            const result = await response.json();

            if (result.success) {
                alert('تم الحجز بنجاح');
                bootstrap.Modal.getInstance(document.getElementById('reservationModal')).hide();
                this.searchCustomer(); // Refresh
            } else {
                alert('فشل الحجز: ' + result.message);
            }
        } catch (e) {
            console.error(e);
            alert('حدث خطأ');
        }
    },

    // ==========================================
    // Sessions Logic
    // ==========================================
    renderSessions: async function() {
        const content = document.getElementById('main-content');
        content.innerHTML = `
            <div class="card">
                <div class="card-header d-flex justify-content-between align-items-center">
                    <span>الحجوزات الحالية</span>
                    <button class="btn btn-sm btn-primary" onclick="app.refreshSessions()">تحديث</button>
                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table table-striped" id="reservations-table">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>العميل</th>
                                    <th>المدرسة</th>
                                    <th>التاريخ</th>
                                    <th>إجراءات</th>
                                </tr>
                            </thead>
                            <tbody id="reservations-list">
                                <tr><td colspan="5" class="text-center">جاري التحميل...</td></tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        `;
        this.refreshSessions();
    },

    refreshSessions: async function() {
        try {
            const response = await fetch('/api/reservations');
            const data = await response.json();
            const tbody = document.getElementById('reservations-list');
            
            if (data.success && data.data) {
                tbody.innerHTML = data.data.map(r => `
                    <tr>
                        <td>${r.reservationId}</td>
                        <td>${r.customerName || r.customerId}</td> 
                        <td>${r.schoolName || r.schoolId}</td>
                        <td>${new Date(r.reservationDate).toLocaleDateString()}</td>
                        <td>
                            <button class="btn btn-sm btn-info" onclick="app.openSessionModal(${r.reservationId})">إضافة جلسة</button>
                        </td>
                    </tr>
                `).join('');
            } else {
                tbody.innerHTML = '<tr><td colspan="5" class="text-center">لا توجد بيانات</td></tr>';
            }
        } catch (e) {
            console.error(e);
        }
    },

    openSessionModal: async function(reservationId) {
        // Fetch instructors and courses if needed
        // For now, basic inputs
        
        const modalHtml = `
            <div class="modal fade" id="sessionModal" tabindex="-1">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">إضافة جلسة للعميل</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                        </div>
                        <div class="modal-body">
                            <form id="session-form">
                                <input type="hidden" id="sess-reservation-id" value="${reservationId}">
                                
                                <div class="mb-3">
                                    <label class="form-label">رقم الدورة (Course ID)</label>
                                    <input type="number" class="form-control" id="sess-course-id" required>
                                </div>
                                <div class="mb-3">
                                    <label class="form-label">رقم المدرب (Instructor ID)</label>
                                    <input type="number" class="form-control" id="sess-instructor-id" required>
                                </div>
                                <div class="mb-3">
                                    <label class="form-label">تاريخ الجلسة</label>
                                    <input type="date" class="form-control" id="sess-date" required>
                                </div>
                                <div class="row">
                                    <div class="col">
                                        <label class="form-label">من</label>
                                        <input type="time" class="form-control" id="sess-start" required>
                                    </div>
                                    <div class="col">
                                        <label class="form-label">إلى</label>
                                        <input type="time" class="form-control" id="sess-end" required>
                                    </div>
                                </div>
                            </form>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">إلغاء</button>
                            <button type="button" class="btn btn-primary" onclick="app.submitSession()">حفظ الجلسة</button>
                        </div>
                    </div>
                </div>
            </div>
        `;

        document.getElementById('modal-container').innerHTML = modalHtml;
        const modal = new bootstrap.Modal(document.getElementById('sessionModal'));
        modal.show();
    },

    submitSession: async function() {
        const reservationId = document.getElementById('sess-reservation-id').value;
        const courseId = document.getElementById('sess-course-id').value;
        const instructorId = document.getElementById('sess-instructor-id').value;
        const date = document.getElementById('sess-date').value;
        const start = document.getElementById('sess-start').value;
        const end = document.getElementById('sess-end').value;

        const payload = {
            reservationId: parseInt(reservationId),
            courseId: parseInt(courseId),
            instructorId: parseInt(instructorId),
            sessionDate: new Date(date).toISOString(),
            startTime: start + ":00",
            endTime: end + ":00",
            attendanceStatus: 0, // Scheduled
            notes: "Created via Dashboard"
        };

        try {
            const response = await fetch('/api/session-attendances', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(payload)
            });
            const result = await response.json();

            if (result.success) {
                alert('تم إضافة الجلسة بنجاح');
                bootstrap.Modal.getInstance(document.getElementById('sessionModal')).hide();
            } else {
                alert('فشل إضافة الجلسة');
            }
        } catch (e) {
            console.error(e);
            alert('حدث خطأ');
        }
    },

    // ==========================================
    // Lookups: Schools
    // ==========================================
    renderSchools: async function() {
        const content = document.getElementById('main-content');
        content.innerHTML = `
            <div class="card">
                <div class="card-header d-flex justify-content-between align-items-center">
                    <span>قائمة المدارس</span>
                    <button class="btn btn-sm btn-success" onclick="app.openSchoolModal()">إضافة مدرسة</button>
                </div>
                <div class="card-body">
                    <ul class="list-group" id="schools-list">Loading...</ul>
                </div>
            </div>
        `;
        
        try {
            const response = await fetch('/api/schools');
            const data = await response.json();
            const list = document.getElementById('schools-list');
            if (data.success && data.data) {
                list.innerHTML = data.data.map(s => `
                    <li class="list-group-item d-flex justify-content-between align-items-center">
                        ${s.schoolName} (${s.address || '-'})
                        <span class="badge bg-primary rounded-pill">${s.schoolId}</span>
                    </li>
                `).join('');
            }
        } catch (e) { console.error(e); }
    },
    
    openSchoolModal: function() {
        // Implementation for adding school (omitted for brevity but structure is same)
        alert('Feature to add school implementation');
    },

    renderLicenses: function() {
        // Similar to Schools
        const content = document.getElementById('main-content');
        content.innerHTML = '<h3>قريباً...</h3>';
    }
};

document.addEventListener('DOMContentLoaded', () => {
    app.init();
});
