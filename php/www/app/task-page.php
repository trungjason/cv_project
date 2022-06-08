<?php
if (session_status() !== PHP_SESSION_ACTIVE) {
    session_start();
}

if (!isset($_POST['action'])) {
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

// Giám đốc không thực hiện được chức năng này
if ($_SESSION['role'] == 'Giám đốc') {
    header('Location: ../index.php');
    exit();
}

?>
<!-- Copy template từ file này này bắt đầu viết code -->
<div class="container">
    <div class="card">
        <div class="card-body">
            <div class="d-flex justify-content-between">
                <h3 class="">Công việc</h3>
                <?php
                if ($_SESSION['role'] == 'Trưởng phòng') {

                ?>
                    <a href="#" onclick="loadEmployeeList()" class="btn btn-primary" data-target="#new-task" data-toggle="modal"><i class="far fa-calendar-plus mr-2"></i>Thêm việc</a>
                <?php
                }
                ?>
            </div>
        </div>
    </div>
    <!-- List Task ở đây -->
    <div id="task-list">

    </div>
</div>

<!-- Modal Thêm task -->
<?php
// Không phải trưởng phòng => không có modal thêm task + đánh giá task này
if ($_SESSION['role'] == "Trưởng phòng") {
?>
    <div class="modal fade" role="dialog" aria-modal="true" id="new-task">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
            <form onsubmit="return false" id="task-info" class="modal-content">
                <div class="modal-header text-center">
                    <h3 class="modal-title m-auto">Thêm công việc mới</h3>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 mb-2">
                            <div class="form-group">
                                <label for="task-title" class="h5">Tiêu đề</label>
                                <input type="text" class="form-control" id="task-title" placeholder="Tên tiêu đề">
                            </div>
                        </div>
                        <div class="col-12 mb-2">
                            <div class="form-group">
                                <label for="employee" class="h5">Nhân viên đảm nhiệm</label>
                                <select id="employee" class="form-select form-control">
                                    <option value="" selected>Chọn nhân viên</option>
                                    <!-- Thêm option nhân viên tại đây -->
                                </select>
                            </div>
                        </div>
                        <div class="col-12 mb-2">
                            <div class="form-group">
                                <label for="deadline" class="h5">Thời hạn</label>
                                <input type="date" class="form-control" id="deadline" value="">
                            </div>
                        </div>
                        <div class="col-12 mb-2">
                            <div class="form-group">
                                <label for="task-desc" class="h5">Mô tả</label>
                                <textarea rows="3" type="text" class="form-control" id="task-desc" placeholder="Mô tả công việc"></textarea>
                            </div>
                        </div>
                        <div class="col-12 mb-2">
                            <div class="form-group">
                                <h5>Tệp đính kèm</h5>
                                <div class="col-10">
                                    <input id="file-placeholder" class="form-control float-left" readonly placeholder="Chưa có tập tin đính kèm nào">
                                    <!-- File list ở đây -->
                                    <div id="custom-file-list" class="custom-file-list">

                                    </div>
                                </div>
                                <div class="col-2 float-right">
                                    <label class="btn btn-primary" for="attachment">
                                        <i class="fas fa-plus"></i>
                                    </label>
                                    <input hidden type="file" name="attachment[]" multiple id="attachment">
                                </div>
                            </div>
                        </div>
                        <div class="col-12">
                            <div id="responseMessage" role="alert">
                            </div>
                        </div>
                        <div class="col-12">
                            <div class="form-group text-center justify-content-between">
                                <button id="reset" type="reset" class="btn btn-danger mt-3">Làm mới</button>
                                <button id="create-task" class="btn btn-primary mt-3">Thêm công việc</button>
                            </div>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    </div>

    <!-- Đánh giá task -->
    <div class="modal fade" role="dialog" aria-modal="true" id="rate-task">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header text-center">
                    <h3 class="modal-title m-auto" id="submit-title">Đánh giá</h3>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-12 mb-2">
                            <div class="form-group">
                                <label for="rate-task-title" class="h5">Nhiệm vụ</label>
                                <input type="text" class="form-control" id="rate-task-title" placeholder="Tên tiêu đề" readonly>
                            </div>
                        </div>
                        <div class="col-md-6 mb-2">
                            <div class="form-group">
                                <label for="rate-deadline" class="h5">Thời hạn</label>
                                <input type="date" class="form-control" id="rate-deadline" value="" readonly>
                            </div>
                        </div>
                        <div class="col-md-6 mb-2">
                            <div class="form-group">
                                <label for="rate-submit-time" class="h5">Thời gian nộp</label>
                                <input type="date" class="form-control" id="rate-submit-time" value="" readonly>
                            </div>
                        </div>
                        <div class="col-12 mb-2">
                            <div class="form-group">
                                <label for="task-rate" class="h5 mr-3">Mức độ hoàn thành</label>
                                <span class="" id="feedback"></span>
                                    <select id="task-rate" class="form-select form-control">
                                        <option value="" selected>chọn mức độ đánh giá</option>
                                        
                                        <!-- Thêm option đánh giá tại đây -->
                                    </select>
                                    
                            </div>
                        </div>
                        <div class="col-12">
                            <div id="rate-responseMessage" role="alert">
                            </div>
                        </div>
                        <div class="col-12">
                            <div class="form-group text-center justify-content-between">
                                <button id="rateTask" class="btn btn-success mt-3">Duyệt</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
<?php
}
?>

<!-- Modal Submit task -->
<div class="modal fade" role="dialog" aria-modal="true" id="form-submit-task">
    <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
        <div class="modal-content">
            <div class="modal-header text-center">
                <h3 class="modal-title m-auto" id="submit-title">Gửi báo cáo/phản hồi</h3>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-12 mb-2">
                        <div class="form-group">
                            <label for="task-submit-title" class="h5">Tiêu đề</label>
                            <input type="text" class="form-control" id="task-submit-title" placeholder="Tên tiêu đề" readonly>
                        </div>
                    </div>
                    <div class="col-12 mb-2">
                        <div class="form-group">
                            <label for="submit-deadline" class="h5">Thời hạn</label>
                            <input type="date" class="form-control" id="submit-deadline" value="" readonly>
                        </div>
                    </div>
                    <div class="col-12 mb-2">
                        <div class="form-group">
                            <label for="task-submit-desc" class="h5">Mô tả</label>
                            <textarea rows="3" type="text" class="form-control" id="task-submit-desc" placeholder="Mô tả công việc" readonly></textarea>
                        </div>
                    </div>
                    <div class="col-12 mb-2">
                        <div class="form-group">
                            <label for="submit-msg" class="h5">Nội dung</label>
                            <textarea rows="2" type="text" class="form-control" id="submit-msg" placeholder="Thông tin và lời nhắn"></textarea>
                        </div>
                    </div>
                    <div class="col-12 mb-2">
                        <div class="form-group">
                            <h5>Tệp đính kèm</h5>
                            <div class="col-10">
                                <input id="submit-file-placeholder" class="form-control float-left" readonly placeholder="Chưa có tập tin đính kèm nào">
                                <!-- File list ở đây -->
                                <div id="submit-custom-file-list" class="custom-file-list">
                                </div>
                            </div>
                            <div class="col-2 float-right">
                                <label class="btn btn-primary" for="submit-attachment">
                                    <i class="fas fa-plus"></i>
                                </label>
                                <input hidden type="file" name="attachment[]" multiple id="submit-attachment">
                            </div>
                        </div>
                    </div>
                    <div class="col-12">
                        <div id="submit-responseMessage" role="alert">
                        </div>
                    </div>
                    <div class="col-12">
                        <div class="form-group text-center justify-content-between">
                            <button onclick="resetSubmitForm()" class="btn btn-danger mt-3">Làm mới</button>
                            <button id="submit-task" class="btn btn-success mt-3">Gửi</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<!-- message dialog -->
<div class="modal fade" id="message-dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header text-center">
                <h4 class="modal-title">Thông báo</h4>
            </div>
            <div class="modal-body">
                <p id="message">Chờ chút nha...</p>
                <div id="progress" class="progress" style="display: none">
                    <div id="progress-bar" class="progress-bar progress-bar-striped progress-bar-animated" style="width: 0%"></div>
                </div>
            </div>
            <div class="modal-footer">
                <button id="upload-complete" class="btn btn-primary" data-dismiss="modal">Đóng</button>
            </div>
        </div>
    </div>
</div>

<!-- Confirm Cancel dialog -->
<div class="modal fade" id="confirm-task-cancel">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header text-center">
                <h4 class="modal-title">Hủy giao nhiệm vụ</h4>
                <button type="button" class="close" data-dismiss="modal">&times;</button>
            </div>

            <div class="modal-body">
                Bạn có chắc rằng muốn hủy giao nhiệm vụ <strong id="task-name"></strong>
                cho nhân viên <strong id="staff-name"></strong> ?
            </div>

            <div class="modal-footer">
                <button type="button" id="confirm" class="btn btn-danger">Xác nhận</button>
                <button type="button" class="btn btn-success" data-dismiss="modal">Không</button>
            </div>
        </div>
    </div>
</div>