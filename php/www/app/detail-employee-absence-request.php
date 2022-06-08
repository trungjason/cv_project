<?php
    if (session_status() !== PHP_SESSION_ACTIVE) {
        session_start();
    }
    
    if(!isset($_POST['action'])){
        header('Location: forbidden.php');
        exit();
    }

    // Nếu chưa đăng nhập thì về trang login
    if (!isset($_SESSION['username'])) {
        header('Location: login.php');
        exit();
    }

    // Nếu chưa đổi mật khẩu khi đăng nhập lần đầu thì không cho vào trang này
    if ($_SESSION['activated'] == 0) {
        header('Location: set-new-password.php');
        exit();
    }
?>

<div class="container">
    <div class="card">
        <div class="card-body">
            <h1 class="text-center text-uppercase" >Chi tiết yêu cầu nghỉ phép</h1>
        </div>
    </div>

    <div class="card">
        <div class="card-body">
            <label class="col-12 mb-3 card-title">
                <h5 >THÔNG TIN DUYỆT ĐƠN</h5>
            </label>

            <div class="row">
                <div class="col-md-6 mb-2">
                    <div class="form-group">
                        <label for="detailAbsenceRequest_status" class="font-weight-bold" style="color : #605C8D !important;">Trạng thái xử lý</label>
                        <input readonly value="" id="detailAbsenceRequest_status" class="form-control">
                    </div>
                </div>

                <div class="col-md-6 mb-2">
                    <div class="form-group">
                        <label for="detailAbsenceRequest_responseTime" class="font-weight-bold" style="color : #605C8D !important;">Ngày duyệt</label>
                        <input readonly value="" id="detailAbsenceRequest_responseTime" class="form-control">
                    </div>
                </div>         
            </div>

            <div class="row">
                <div class="col-md-12 mb-2">
                    <div class="form-group">
                        <label for="detailAbsenceRequest_responseMessage" class="font-weight-bold" style="color : #605C8D !important;">Tin phản hồi</label>
                        <textarea readonly id="detailAbsenceRequest_responseMessage" class="form-control"></textarea>
                    </div>
                </div>
            </div>

            <label class="col-12 mb-3 card-title">
                <h5 >THÔNG TIN NGHỈ PHÉP</h5>
            </label>

            <div class="row">
                <div class="col-md-6 mb-2">
                    <div class="form-group">
                        <label for="detailAbsenceRequest_requestID" class="font-weight-bold" style="color : #605C8D !important;">Mã yêu cầu</label>
                        <input readonly value="" id="detailAbsenceRequest_requestID" class="form-control">
                    </div>
                </div>

                <div class="col-md-6 mb-2">
                    <div class="form-group">
                        <label for="detailAbsenceRequest_requestTime" class="font-weight-bold" style="color : #605C8D !important;">Ngày gửi yêu cầu</label>
                        <input readonly value="" id="detailAbsenceRequest_requestTime" class="form-control">
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-4 mb-2">
                    <div class="form-group">
                        <label for="detailAbsenceRequest_dateBegin" class="font-weight-bold" style="color : #605C8D !important;">Ngày bắt đầu</label>
                        <input readonly value="" id="detailAbsenceRequest_dateBegin" class="form-control">
                    </div>
                </div>

                <div class="col-md-4 mb-2">
                    <div class="form-group">
                        <label for="detailAbsenceRequest_dateEnd" class="font-weight-bold" style="color : #605C8D !important;">Ngày kết thúc</label>
                        <input readonly value="" id="detailAbsenceRequest_dateEnd" class="form-control">
                    </div>
                </div>

                <div class="col-md-4 mb-2">
                    <div class="form-group">
                        <label for="detailAbsenceRequest_absenceDays" class="font-weight-bold" style="color : #605C8D !important;">Số ngày xin nghỉ</label>
                        <input readonly value="" id="detailAbsenceRequest_absenceDays" class="form-control">
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12 mb-2">
                    <div class="form-group">
                        <label for="detailAbsenceRequest_absenceReason" class="font-weight-bold" style="color : #605C8D !important;">Lí do nghỉ phép</label>
                        <textarea readonly id="detailAbsenceRequest_absenceReason" class="form-control"></textarea>
                    </div>
                </div>
            </div>

            <div class="row">
                
            </div>

            <label class="col-12 mb-3 card-title">
                <h5 >TỆP ĐÍNH KÈM</h5>
            </label>

            <div class="row">
                <div class="col-md-12 mb-2">
                    <!-- Danh sách tệp đính kèm -->
                    <table class="table rounded table-hover mt-3 mb-0" id="absence-request-table">
                        <thead>
                            <tr>
                                <th style="border-top: None;" class="text-center text-uppercase">Tên tập tin</th>
                                <th style="border-top: None;" class="text-center text-uppercase">Kiểu tập tin</th>
                                <th style="border-top: None;" class="text-center text-uppercase">Kích thước</th>
                                <th style="border-top: None;" class="text-center text-uppercase">Download</th>
                            </tr>
                        </thead>

                        <tbody id="absence_request_attachment_table">
                            <!-- let js do its job -->
                        <tbody>
                    </table>
                </div>
            </div>

            <label class="col-12 mb-3 card-title">
                <h5 >THÔNG TIN NHÂN VIÊN</h5>
            </label>

            <div class="row">
                <div class="col-md-6 mb-2">
                    <div class="form-group">
                        <label for="detailAbsenceRequest_userID" class="font-weight-bold" style="color : #605C8D !important;">Mã số nhân viên</label>
                        <input readonly value="" id="detailAbsenceRequest_userID" class="form-control">
                    </div>
                </div>

                <div class="col-md-6 mb-2">
                    <div class="form-group">
                        <label for="detailAbsenceRequest_fullName" class="font-weight-bold" style="color : #605C8D !important;">Họ và Tên</label>
                        <input readonly value="" id="detailAbsenceRequest_fullName" class="form-control">
                    </div>
                </div>                            
            </div>

            <div class="row">
                <div class="col-md-6 mb-2">
                    <div class="form-group">
                        <label for="detailAbsenceRequest_gender" class="font-weight-bold" style="color : #605C8D !important;">Giới tính</label>
                        <input readonly value="" id="detailAbsenceRequest_gender" class="form-control">
                    </div>
                </div>

                <div class="col-md-6 mb-2">
                    <div class="form-group">
                        <label for="detailAbsenceRequest_DOB" class="font-weight-bold" style="color : #605C8D !important;">Ngày sinh</label>
                        <input readonly value="" id="detailAbsenceRequest_DOB" class="form-control">
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-6 mb-2">
                    <div class="form-group">
                        <label for="detailAbsenceRequest_phoneNumber" class="font-weight-bold" style="color : #605C8D !important;">Số điện thoại</label>
                        <input readonly value="" id="detailAbsenceRequest_phoneNumber" class="form-control">
                    </div>
                </div>

                <div class="col-md-6 mb-2">
                    <div class="form-group">
                        <label for="detailAbsenceRequest_email" class="font-weight-bold" style="color : #605C8D !important;">Email cá nhân</label>
                        <input readonly value="" id="detailAbsenceRequest_email" class="form-control">
                    </div>
                </div>
            </div>
                      
            <label class="col-12 mb-3 card-title">
                <h5 >THÔNG TIN PHÒNG BAN</h5>
            </label>

            <div class="row">
                <div class="col-md-6 mb-2">
                    <div class="form-group">
                        <label for="detailAbsenceRequest_departmentID" class="font-weight-bold" style="color : #605C8D !important;">Mã phòng ban</label>
                        <input readonly value="" id="detailAbsenceRequest_departmentID" class="form-control">
                    </div>
                </div>

                <div class="col-md-6 mb-2">
                    <div class="form-group">
                        <label for="detailAbsenceRequest_departmentRoomID" class="font-weight-bold" style="color : #605C8D !important;">Văn phòng</label>
                        <input readonly value="" id="detailAbsenceRequest_departmentRoomID" class="form-control">
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-6 mb-2">
                    <div class="form-group">
                        <label for="detailAbsenceRequest_departmentName" class="font-weight-bold" style="color : #605C8D !important;">Tên phòng ban</label>
                        <input readonly value="" id="detailAbsenceRequest_departmentName" class="form-control">
                    </div>
                </div>

                <div class="col-md-6 mb-2">
                    <div class="form-group">
                        <label for="detailAbsenceRequest_role" class="font-weight-bold" style="color : #605C8D !important;">Chức vụ</label>
                        <input readonly value="" id="detailAbsenceRequest_role" class="form-control">
                    </div>
                </div>
            </div>                     
        </div>  
    </div>   
</div>


<!-- Modal Dialog -->

<!-- message dialog -->
