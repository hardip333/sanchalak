app.controller('FeeCategoryChangeCtrl', function ($scope, $http, $rootScope, $state, $cookies, Upload, $mdDialog, $localStorage, NgTableParams, httpRequestInterceptor) {
    var tokenCookie = $cookies.get('token');
    if (!tokenCookie) {
        localStorage.clear();
        $state.go('login');
    }
    $rootScope.pageTitle = "Fee Category Change";
    $scope.FCC = {}; 
   

    var today = new Date();
    var date = today.getDate();
    var month = today.getMonth() + 1;
    var year = today.getFullYear();
 
    $scope.SubmitApplicationId = function () {
        if ($scope.FCC.ApplicationId == null || $scope.FCC.ApplicationId === undefined) {
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Kindly Enter Application ID")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );

        }
        else {
            $http({
                method: 'POST',
                url: 'api/FeeCategoryChange/FeeCategoryChange',
                data: $scope.FCC,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    if (response.response_code == "0") {
                        $state.go('login');
                    }
                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    if (response.response_code == "201") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        $mdDialog.show(
                            $mdDialog.alert()
                                .parent(angular.element(document.querySelector('#popupContainer')))
                                .clickOutsideToClose(true)
                                .title("Alert")
                                .textContent(response.obj)
                                .ariaLabel('Alert Dialog Demo')
                                .ok('Okay')
                        );
                        $scope.FCC = {};
                        $scope.FCCList = {};
                        $scope.DropdownTotalAmount = {};
                        $scope.Showbuttonflag = false;
                        $scope.Showbuttonflag1 = false;
                        $scope.Showbuttonflag2 = false;
                    }
                    else {
                        $scope.FCCList = response.obj[0];
                        $scope.Showbuttonflag = true;
                        $scope.DropdownTotalAmount = {};
                        $scope.Showbuttonflag1 = false;
                        $scope.Showbuttonflag2 = false;

                    }

                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.FeeCategoryChange = function (ev, data) {

        if ($scope.FCCList.IsAdmissionFeePaid == true) {
            var confirm = $mdDialog.confirm()
                .title("Applicant Has Paid Fees, Are You Sure to procced for Fee Category Change ?")
                .textContent('')
                .ariaLabel('Lucky Day')
                .targetEvent(ev)
                .ok('Yes')
                .cancel('No');
            $mdDialog.show(confirm).then(function () {

                $scope.FCC = data;
                $http({
                    method: 'POST',
                    url: 'api/FeeCategoryChange/FeeCategoryChange1',
                    data: $scope.FCC,
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        if (response.response_code == "0") {
                            $state.go('login');
                        }
                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
                            $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        }
                        if (response.response_code == "201") {
                            $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                            alert(response.obj);
                        }
                        else {
                            $scope.NewFCCList = response.obj;
                            $scope.Showbuttonflag1 = true;
                        }

                    })
                    .error(function (res) {
                        $rootScope.broadcast('dialog', "Error", "alert", res.obj);
                    });

            }, function () {
                $scope.status = 'You decided not to change Fee Category.';
                $scope.Showbuttonflag1 = false;
                alert($scope.status);
            });
        }
        else {
            $http({
                method: 'POST',
                url: 'api/FeeCategoryChange/FeeCategoryChange1',
                data: $scope.FCC,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    if (response.response_code == "0") {
                        $state.go('login');
                    }
                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    if (response.response_code == "201") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        alert(response.obj);
                    }
                    else {
                        $scope.NewFCCList = response.obj;
                        $scope.Showbuttonflag1 = true;
                    }

                })
                .error(function (res) {
                    $rootScope.broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.ShowTotalAmount = function () {
        $scope.DropdownTotalAmount = {};
        for (var i in $scope.NewFCCList) {
            if ($scope.FCC.FeeCategoryPartTermMapId == $scope.NewFCCList[i].FeeCategoryPartTermMapId) {
                $scope.DropdownTotalAmount = $scope.NewFCCList[i];
                $scope.Showbuttonflag2 = true;
            }
        }
        
    };

    $scope.ValidateIFSCCode = function () {
        $scope.ShowBankDetails = false;

        if ($scope.FCC.Bank.Id == 1) {
            $scope.objIFSC = {};
            $scope.objIFSC.IFSCCode = $scope.FCC.IFSCCode.substring(0, 5);
            if ($scope.objIFSC.IFSCCode != "BARB0") {
                alert("Please select Bank of Baroda IFSCCode.");
                $scope.ShowBankDetails = true;
            }
        }
    };

    $scope.FinalFeeChange = function () {

        $scope.CountAccNumber = $("#AccountNumber").val().length;

        if ($scope.FCC.IFSCCode == null || $scope.FCC.IFSCCode == undefined || $scope.FCC.IFSCCode == "") {
            $scope.FCC.IsBOBAccount = null;
            $scope.obj1 = {};
            $scope.obj1.IFSCCode = null;
        }
        else {
            $scope.obj1 = {};
            $scope.obj1.IFSCCode = $scope.FCC.IFSCCode.substring(0, 5);
            if ($scope.obj1.IFSCCode == "BARB0") {
                $scope.FCC.IsBOBAccount = true;
            }
            else {
                $scope.FCC.IsBOBAccount = false;
            }
        }

        
        if (($scope.FCCList.IsAdmissionFeePaid == true) && ($localStorage.PassbookDoc != null || $localStorage.PassbookDoc != undefined || $localStorage.PassbookDoc != "")) {
            $cookies.put("PassbookDoc", $localStorage.PassbookDoc);
        }
        else if (($scope.FCCList.IsAdmissionFeePaid == true && $scope.obj1.IFSCCode != "BARB0") && ($localStorage.RTGSForm != null || $localStorage.RTGSForm != undefined || $localStorage.RTGSForm != "")) {
            $cookies.put("RTGSForm", $localStorage.RTGSForm);
        }
        //else
        //{
        //    $state.go('FeeCategoryChange');
        //}
   

       

        //$scope.FCC.PassbookDoc = $scope.PassbookDoc;

        var Passbook = $("#SuccessMsgPassbookImg").text();

        if (($scope.FCCList.IsAdmissionFeePaid == true) && (Passbook == null || Passbook == undefined || Passbook == "")) {
            alert("File Not Uploaded Or File is Not in Proper Format");
            return false;
        }
       
        if ($scope.PassbookDoc == null || $scope.PassbookDoc == undefined || $scope.PassbookDoc == "") {
            $scope.FCC.PassbookDoc = null;
        }
        else if ($scope.PassbookDoc == "[object Object]") {
            $scope.FCC.PassbookDoc = null;
        }
        else {
            $scope.FCC.PassbookDoc = $scope.PassbookDoc;
        }


        var RTGSForm = $("#SuccessMsgRTGSForm").text();

        if (($scope.FCCList.IsAdmissionFeePaid == true && $scope.obj1.IFSCCode != "BARB0") && (RTGSForm == null || RTGSForm == undefined || RTGSForm == "")) {
            alert("File Not Uploaded Or File is Not in Proper Format");
            return false;
        }

        if ($scope.RTGSForm == null || $scope.RTGSForm == undefined || $scope.RTGSForm == "") {
            $scope.FCC.RTGSForm = null;
        }
        else if ($scope.RTGSForm == "[object Object]") {
            $scope.FCC.RTGSForm = null;
        }
        else {
            $scope.FCC.RTGSForm = $scope.RTGSForm;
        }

        $scope.FCC.ApplicationId = $scope.FCCList.ApplicationId;
        $scope.FCC.OldFeeCategoryId = $scope.FCCList.FeeCategoryId;
        //$scope.FCC.IsVerificationSms = $scope.FCCList.IsVerificationSms;
        //$scope.FCC.IsVerificationEmail = $scope.FCCList.IsVerificationEmail;
        //$scope.FCC.IsVerificationSMSOnView = $scope.FCCList.IsVerificationSMSOnView;
        //$scope.FCC.IsVerificationEmailOnView = $scope.FCCList.IsVerificationEmailOnView;
        $scope.FCC.IsAdmissionFeePaid = $scope.FCCList.IsAdmissionFeePaid;
        $scope.FCC.IsPRNGenerated = $scope.FCCList.IsPRNGenerated;
        $scope.FCC.ShortIFSCCode = $scope.obj1.IFSCCode;

        //var SMSDate = $scope.FCC.IsVerificationSMSOnView.split("-");
        //$scope.FCC.IsVerificationSmsOn = new Date(SMSDate[2], (SMSDate[1] >= 1) ? (SMSDate[1] - 1) : SMSDate[1], SMSDate[0]);

        //var EmailDate = $scope.FCC.IsVerificationEmailOnView.split("-");
        //$scope.FCC.IsVerificationEmailOn = new Date(EmailDate[2], (EmailDate[1] >= 1) ? (EmailDate[1] - 1) : EmailDate[1], EmailDate[0]);

        //if ($scope.FCC.IsVerificationSmsOn == 'Invalid Date' || $scope.FCC.IsVerificationSmsOn == undefined || $scope.FCC.IsVerificationSmsOn == "") {
        //    $scope.FCC.IsVerificationSmsOn = null;
        //}
        //if ($scope.FCC.IsVerificationEmailOn == 'Invalid Date' || $scope.FCC.IsVerificationEmailOn == undefined || $scope.FCC.IsVerificationEmailOn == "") {
        //    $scope.FCC.IsVerificationEmailOn = null;
        //}

        if (($scope.FCCList.IsAdmissionFeePaid == true)) {
            if (($scope.FCC.Bank == null || $scope.FCC.Bank == "" || $scope.FCC.Bank === undefined)) {
                alert("Please Select Bank Name...!");
            }
            else {
                $scope.FCC.BankId = $scope.FCC.Bank.Id;
                $scope.FCC.BankName = $scope.FCC.Bank.BankName;
            }
        }

        
        if (($scope.FCCList.IsAdmissionFeePaid == true) && ($localStorage.PassbookDoc != null || $localStorage.PassbookDoc != undefined || $localStorage.PassbookDoc != "")) {
            $cookies.put("PassbookDoc", $localStorage.PassbookDoc);
        }
        else if (($scope.FCCList.IsAdmissionFeePaid == true && $scope.obj1.IFSCCode != "BARB0") && ($localStorage.RTGSForm != null || $localStorage.RTGSForm != undefined || $localStorage.RTGSForm != "")) {
            $cookies.put("RTGSForm", $localStorage.RTGSForm);
        }


        if (($scope.FCCList.IsAdmissionFeePaid == true) && ($scope.CountAccNumber < 10)) {
            alert("Account Number should be at least having 10 digit...!")
        }
        else if (($scope.FCCList.IsAdmissionFeePaid == true) && ($scope.FCC.AccountNumber == null || $scope.FCC.AccountNumber == "" || $scope.FCC.AccountNumber === undefined)) {
            alert("Please Add Account Number...!");
        }
        else if (($scope.FCCList.IsAdmissionFeePaid == true) && ($scope.FCC.AccountName == null || $scope.FCC.AccountName == "" || $scope.FCC.AccountName === undefined)) {
            alert("Please Add Account Name...!");
        }
        //else if (($scope.FCCList.IsAdmissionFeePaid == true) && ($scope.FCC.Bank == null || $scope.FCC.Bank == "" || $scope.FCC.Bank === undefined)) {
        //    alert("Please Select Bank Name...!");
        //}
        else if (($scope.FCCList.IsAdmissionFeePaid == true) && ($scope.FCC.IFSCCode == null || $scope.FCC.IFSCCode == "" || $scope.FCC.IFSCCode === undefined)) {
            alert("Please Add IFSC CODE...!");
        }
        else if (($scope.FCCList.IsAdmissionFeePaid == true) && ($scope.FCC.PassbookDoc == null || $scope.FCC.PassbookDoc == "" || $scope.FCC.PassbookDoc === undefined)) {
            alert("Please Add Passbook Photo...!");
        }
        else if (($scope.FCCList.IsAdmissionFeePaid == true && $scope.obj1.IFSCCode != "BARB0") && ($scope.FCC.RTGSForm == null || $scope.FCC.RTGSForm == "" || $scope.FCC.RTGSForm === undefined)) {
            alert("Please Add RTGS Form...!");
        }
        else if ($scope.FCC.FeeCategoryChangeRemark == null || $scope.FCC.FeeCategoryChangeRemark == "" || $scope.FCC.FeeCategoryChangeRemark === undefined) {
            alert("Please Add Fee Category Change Remarks...!");
        }
        else if (($scope.FCCList.IsAdmissionFeePaid == true) && ($scope.obj1.IFSCCode == "BARB0" && $scope.FCC.BankId != 1)) {
            $scope.ValidateIFSCCode();
            alert("Please Enter Valid IFSC Code or Bank Name.!!");
        }
        else if (($scope.FCCList.IsAdmissionFeePaid == true) && ($scope.FCC.BankId == 1 && $scope.obj1.IFSCCode != "BARB0")) {
            $scope.ValidateIFSCCode();
        }
        //else if (($scope.FCCList.IsAdmissionFeePaid == true) && ($scope.RealBankName != $scope.FCC.BankName)) {
        //    $scope.ValidateIFSCCode();
        //    alert("Please Enter Valid IFSC Code or Bank Name.!!");
        //}
        else {
            $http({
                method: 'POST',
                url: 'api/FeeCategoryChange/AdmissionFeeCategoryChangeAdd',
                data: $scope.FCC,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    if (response.response_code == "0") {
                        $state.go('login');
                    }
                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);

                    }
                    if (response.response_code == "201") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        //alert(response.obj);
                        $mdDialog.show(
                            $mdDialog.alert()
                                .parent(angular.element(document.querySelector('#popupContainer')))
                                .clickOutsideToClose(true)
                                .title("Alert")
                                .textContent(response.obj)
                                .ariaLabel('Alert Dialog Demo')
                                .ok('Okay')
                        );
                        $scope.FCC = {};
                        $scope.PassbookDoc = {};
                        $scope.RTGSForm = {};
                        $scope.FCCList = {};
                        $scope.DropdownTotalAmount = {};
                        $scope.Showbuttonflag = false;
                        $scope.Showbuttonflag1 = false;
                        $scope.Showbuttonflag2 = false;
                        $localStorage.PassbookDoc = {};
                        $localStorage.RTGSForm = {};

                    }
                    else {
                        alert(response.obj);
                        $scope.FCC = {};
                        $scope.PassbookDoc = {};
                        $scope.RTGSForm = {};
                        $scope.FCCList = {};
                        $scope.DropdownTotalAmount = {};
                        $scope.Showbuttonflag = false;
                        $scope.Showbuttonflag1 = false;
                        $scope.Showbuttonflag2 = false;
                        $localStorage.PassbookDoc = {};
                        $localStorage.RTGSForm = {};

                    }

                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
       
    };


    $scope.getRequest = function () {
        $scope.obj = {};
        $scope.obj.IFSCCode = $scope.FCC.IFSCCode; var request = {
            method: 'get',
            url: 'https://ifsc.razorpay.com/' + $scope.obj.IFSCCode,
            dataType: 'json',
            contentType: "application/json"
        }
        $scope.arrBirds = new Array; $http(request)
            .success(function (response) {
                $scope.arrBirds = response;
                $scope.list = $scope.arrBirds;
                $scope.IsVisible = true;
                $scope.IsError = false;
                $scope.RealBankName = $scope.list.BANK;
            })
            .error(function () {
                $scope.IsError = true;
                $scope.IsVisible = false;
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                $scope.IsVisible = false;
            });
    };

    $("#ifsc").on("keyup", function () {
        var ifsc = $("#ifsc").val();
        var ifscCheck = document.getElementById("ifscCheck");
        var regexp = /[A-Z|a-z]{4}[0][a-zA-Z0-9]{6}$/;
        if (regexp.test(ifsc)) {
            ifscCheck.innerHTML = "";
        }
        else {
            ifscCheck.innerHTML = "Please Try Again ! IFSC Code Format Is Not Valid";
        }
    });

    //Genric Config for file size-Dynamic Validation
    $scope.byId = function (data) {
        return $http({
            method: 'POST',
            url: 'api/FeeCategoryChange/GenericConfigurationGetById',
            data: { Id: data },
            headers: { "Content-Type": 'application/json' }
        })
    };

    $scope.UploadStudentPassBookImage = function ($files) {

        $scope.SelectedFiles = $files;

        var fileExtension = '.' + $scope.SelectedFiles[0].name.split('.').pop();
        //var name = $scope.SelectedFiles[0].name.split('.').slice(0, -1);
        $scope.PassbookDoc = {};
        $scope.PassbookDoc = $scope.FCCList.ApplicationId + "_" + "PassBook" + "_" + "FeeChange" + "_" + date + "_" + month + "_" + year + fileExtension;
        $localStorage.PassbookDoc = $scope.PassbookDoc;

        $cookies.put("PassbookDoc", $scope.PassbookDoc);



        if ($scope.SelectedFiles && $scope.SelectedFiles.length) {

            var fileCheck = document.getElementById("file1").value;

            //var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.bmp)$/i;
            var allowedExtensions = /(\.jpg|\.jpeg)$/i;
            const fi = document.getElementById("file1");

            if (!allowedExtensions.exec(fileCheck)) {

                document.getElementById("ErrorMsgPassbookImg").innerHTML = "It only accepts Jpeg file";
                document.getElementById("SuccessMsgPassbookImg").innerHTML = "";
                fileCheck.value = '';
                return false;
            }

            if (fi.files.length > 0) {
                var cnt = fi.files.length;
                //$scope.byId(6).then(function (response) {
                // $scope.minVal = response.data.obj[0].Value;

                $scope.byId(5).then(function (response) {
                    $scope.maxVal = response.data.obj[0].Value;
                    for (i = 0; i < cnt; i++) {

                        const fsize = fi.files.item(i).size;
                        const file = Math.round((fsize / 1024));
                        if (file >= $scope.maxVal) {
                            document.getElementById("ErrorMsgPassbookImg").innerHTML = "Please select a file below 512KB";
                            document.getElementById("SuccessMsgPassbookImg").innerHTML = "";
                            return false;
                        }
                        else {
                            Upload.upload({
                                url: 'api/FeeCategoryChange/UploadPassbookPhoto',
                                data: {
                                    files: $scope.SelectedFiles
                                }
                            }).then(function (response) {

                                if (response.data.response_code == "200") {

                                    $scope.Result = response.data;
                                    document.getElementById("ErrorMsgPassbookImg").innerHTML = "";
                                    document.getElementById("SuccessMsgPassbookImg").innerHTML = "File Uploaded Successfully.";
                                }
                                else {

                                    $scope.PassbookDoc = undefined;
                                    $("#file1").val("");
                                    alert("File Upload Failed!");
                                    return false;
                                }

                            }, function (response) {
                                if (response.status > 0) {
                                    var errorMsg = response.status + ': ' + response.data;

                                }
                            }, function (evt) {
                                var element = angular.element(document.querySelector('#dvProgress'));
                                $scope.Progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
                                element.html('<div style="width: ' + $scope.Progress + '%">' + $scope.Progress + '%</div>');
                            });
                        }
                    }
                }).catch(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });

            }
        }
    };

    /*Function for Get Bank List*/
    $scope.getBankList = function () {

        $http({
            method: 'POST',
            url: 'api/FeeCategoryChange/GetBankNameList',
            headers: { "Content-Type": 'application/json' }
        })

            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.BankList = {};
                }
                else {
                    $scope.BankList = response.obj;
                }

            })
            .error(function (res) {

            });
    };

    $scope.UploadStudentRTGSForm = function ($files) {

        $scope.SelectedFiles = $files;

        var fileExtension = '.' + $scope.SelectedFiles[0].name.split('.').pop();
        //var name = $scope.SelectedFiles[0].name.split('.').slice(0, -1);
        $scope.RTGSForm = {};
        $scope.RTGSForm = $scope.FCCList.ApplicationId + "_" + "RTGSForm" + "_" + "FeeChange" + "_" + date + "_" + month + "_" + year + fileExtension;
        $localStorage.RTGSForm = $scope.RTGSForm;

        $cookies.put("RTGSForm", $scope.RTGSForm);



        if ($scope.SelectedFiles && $scope.SelectedFiles.length) {

            var fileCheck = document.getElementById("RTGSForm1").value;

            //var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.bmp)$/i;
            var allowedExtensions = /(\.pdf)$/i;
            const fi = document.getElementById("RTGSForm1");

            if (!allowedExtensions.exec(fileCheck)) {

                document.getElementById("ErrorMsgRTGSForm").innerHTML = "It only accepts pdf file";
                document.getElementById("SuccessMsgRTGSForm").innerHTML = "";
                fileCheck.value = '';
                return false;
            }

            if (fi.files.length > 0) {
                var cnt = fi.files.length;
                //$scope.byId(6).then(function (response) {
                // $scope.minVal = response.data.obj[0].Value;

                $scope.byId(5).then(function (response) {
                    $scope.maxVal = response.data.obj[0].Value;
                    for (i = 0; i < cnt; i++) {

                        const fsize = fi.files.item(i).size;
                        const file = Math.round((fsize / 1024));
                        if (file >= $scope.maxVal) {
                            document.getElementById("ErrorMsgRTGSForm").innerHTML = "Please select a file below 512KB";
                            document.getElementById("SuccessMsgRTGSForm").innerHTML = "";
                            return false;
                        }
                        else {
                            Upload.upload({
                                url: 'api/FeeCategoryChange/UploadRTGSForm',
                                data: {
                                    files: $scope.SelectedFiles
                                }
                            }).then(function (response) {

                                if (response.data.response_code == "200") {

                                    $scope.Result = response.data;
                                    document.getElementById("ErrorMsgRTGSForm").innerHTML = "";
                                    document.getElementById("SuccessMsgRTGSForm").innerHTML = "File Uploaded Successfully.";
                                }
                                else {

                                    $scope.RTGSForm = undefined;
                                    $("#RTGSForm1").val("");
                                    alert("File Upload Failed!");
                                    return false;
                                }

                            }, function (response) {
                                if (response.status > 0) {
                                    var errorMsg = response.status + ': ' + response.data;

                                }
                            }, function (evt) {
                                var element = angular.element(document.querySelector('#dvProgress'));
                                $scope.Progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
                                element.html('<div style="width: ' + $scope.Progress + '%">' + $scope.Progress + '%</div>');
                            });
                        }
                    }
                }).catch(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });

            }
        }
    };

});