app.controller('BranchChangeCtrl', function ($scope, $http, $rootScope, $state, $cookies, Upload, $mdDialog, $localStorage, NgTableParams) {
    var tokenCookie = $cookies.get('token');
    if (!tokenCookie) {
        localStorage.clear();
        $state.go('login');
    }
    $rootScope.pageTitle = "Branch Change";
    $scope.branchChange = {};
    $scope.showDetailflag = false;
    $scope.ShowChangeFlag = false;
    $scope.ShowLableFlag = false;
    
    $scope.checkflag = false;

    $scope.SubmitApplicationId = function () {
        if ($scope.branchChange.ApplicationId == null || $scope.branchChange.ApplicationId === undefined) {
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
                url: 'api/BranchChangeRequest/BranchChangeRequestGet',
                data: $scope.branchChange,
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
                        $scope.applicantDetail = response.obj[0];
                        //$scope.applicantDetail.ApplicationId = $scope.branchChange.ApplicationId;
                        $scope.showDetailflag = true;
                    }

                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };
    
    
    $scope.ChangeBranch = function (ev) {
        //$scope.applicantDetail.SameFeeCategory = false;
        if (($scope.applicantDetail.IsVerificationSms == true || $scope.applicantDetail.IsVerificationEmail == true) && ($scope.applicantDetail.IsAdmissionFeePaid == true)) {
            var confirm = $mdDialog.confirm()
                .title('Applicant Has Paid Fees, Are You Sure to procced for Branch Change?')
                .textContent('')
                .ariaLabel('Lucky day')
                .targetEvent(ev)
                .ok('Yes')
                .cancel('No');

            $mdDialog.show(confirm).then(
                function () {
                    $scope.ShowChangeFlag = true;
                    $scope.ShowLableFlag = false;
                    $scope.applicantDetail.UserConfirmAfterFee = true;
                    $scope.GetBranch();
                },
                function () {
                    $scope.ShowChangeFlag = false;
                    $scope.ShowLableFlag = true;
                }
            );
        }
        else {
            $scope.ShowChangeFlag = true;
            $scope.GetBranch();
        }
    };

    $scope.checkSameFeeCategory = function () {
        debugger;
        console.log($scope.applicantDetail.SameFeeCategory);
        if ($scope.applicantDetail.SameFeeCategory == true) {
            $scope.checkflag = true;
            
        }
        else {
            $scope.checkflag = false;
            
        }

    };
    /*Function for Get Bank List*/
    $scope.getBankList = function () {
        $scope.BankList = {};
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
    $scope.GetBranch = function () {
        if ($scope.applicantDetail.DestinationIncProgInstPartTermId == null || $scope.applicantDetail.DestinationIncProgInstPartTermId === undefined) {
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Kindly Try Again")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
            $http({
                method: 'POST',
                url: 'api/BranchChangeRequest/GetNewBranchList',
                data: $scope.applicantDetail,
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
                        $scope.BranchList = response.obj;

                    }

                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };
    $scope.GetFee = function () {
        if ($scope.applicantDetail.SameFeeCategory == false) {

            if ($scope.applicantDetail.DestinationIncProgInstPartTermId == null || $scope.applicantDetail.DestinationIncProgInstPartTermId === undefined
                || $scope.applicantDetail.FeeCategoryPartTermMapId == null || $scope.applicantDetail.FeeCategoryPartTermMapId === undefined
                || $scope.applicantDetail.FeeCategoryId == null || $scope.applicantDetail.FeeCategoryId === undefined) {
                alert("Please Try Again.");
            }
            else {

                $http({
                    method: 'POST',
                    url: 'api/BranchChangeRequest/GetNewFeeCategoryList',
                    data: $scope.applicantDetail,
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
                            $scope.FeeList = response.obj;

                        }

                    })
                    .error(function (res) {
                        $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                    });
            }
        }
    };
    $scope.getRequest = function () {
        $scope.obj = {};
        
        $scope.obj.IFSCCode = $scope.applicantDetail.IFSCCode; var request = {
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
                $scope.applicantDetail.BankName = $scope.list.BANK;
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
    $scope.UploadStudentRTGSForm = function ($files) {

        $scope.SelectedFiles = $files;

        var fileExtension = '.' + $scope.SelectedFiles[0].name.split('.').pop();
        //var name = $scope.SelectedFiles[0].name.split('.').slice(0, -1);
        $scope.RTGSForm = {};
        $scope.RTGSForm = $scope.applicantDetail.ApplicationId + "_" + "RTGSForm" + "_" + "BRANCH_CHANGE" + "_" + fileExtension;
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
                                url: 'api/BranchChangeRequest/UploadRTGSForm',
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
    $scope.UploadStudentPassBookImage = function ($files) {

        $scope.SelectedFiles = $files;
        console.log($scope.SelectedFiles);
        var fileExtension = '.' + $scope.SelectedFiles[0].name.split('.').pop();
        //var name = $scope.SelectedFiles[0].name.split('.').slice(0, -1);
        $scope.PassbookDoc = {};
        $scope.PassbookDoc = $scope.applicantDetail.ApplicationId + "_" + "PassBook" + "_" + "BRANCH_CHANGE" + fileExtension;
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
                                url: 'api/BranchChangeRequest/UploadPassbookPhoto',
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
    //Genric Config for file size-Dynamic Validation
    $scope.byId = function (data) {
        return $http({
            method: 'POST',
            url: 'api/BranchChangeRequest/GenericConfigurationGetById',
            data: { Id: data },
            headers: { "Content-Type": 'application/json' }
        })
    };


    $scope.CheckData = function () {
        debugger;
        let flag = true;
        
        if ($scope.applicantDetail.DestinationIncProgInstPartTermId == null || $scope.applicantDetail.DestinationIncProgInstPartTermId === undefined
            || $scope.applicantDetail.FeeCategoryPartTermMapId == null || $scope.applicantDetail.FeeCategoryPartTermMapId === undefined) {
            alert("Please Try Again.");
            flag = false;
        }
        else if ($scope.applicantDetail.BranchChangeRemark == null || $scope.applicantDetail.BranchChangeRemark === undefined) {
                alert("Please Enter Remarks.");
                flag = false;
        }
        else {
            if ($scope.applicantDetail.IsAdmissionFeePaid == true && $scope.applicantDetail.UserConfirmAfterFee == true) {
                if ($scope.applicantDetail.SameFeeCategory == true) {
                    if (flag == true) {
                        $scope.SaveBranchChange();
                    }
                }
                else {
                    $scope.applicantDetail.ShortIFSCCode = $scope.applicantDetail.IFSCCode.substring(0, 5);
                    if ($scope.applicantDetail.ShortIFSCCode == "BARB0") {
                        $scope.applicantDetail.IsBOBAccount = true;
                    }
                    else {
                        $scope.applicantDetail.IsBOBAccount = false;
                    }
                    $scope.applicantDetail.PassbookDoc = $scope.PassbookDoc;
                    $scope.applicantDetail.RTGSForm = $scope.RTGSForm;

                    if ($scope.applicantDetail.PassbookDoc == null || $scope.applicantDetail.PassbookDoc === undefined
                        || $scope.applicantDetail.RTGSForm == null || $scope.applicantDetail.RTGSForm === undefined) {
                        alert("Please Try Again.");
                        flag = false;
                    }
                    else if ($scope.applicantDetail.AccountNumber == null || $scope.applicantDetail.AccountNumber === undefined) {
                        alert("Please Enter Account Number.");
                        flag = false;
                    }
                    else if ($scope.applicantDetail.AccountName == null || $scope.applicantDetail.AccountName === undefined) {
                        alert("Please Enter Account Name.");
                        flag = false;
                    }
                    else if ($scope.applicantDetail.BankId == null || $scope.applicantDetail.BankId === undefined) {
                        alert("Please Select Bank Name.");
                        flag = false;
                    }
                    else if ($scope.applicantDetail.BankId == null || $scope.applicantDetail.BankId === undefined) {
                        alert("Please Select Bank Name.");
                        flag = false;
                    }
                    else if ($scope.applicantDetail.IFSCCode == null || $scope.applicantDetail.IFSCCode === undefined) {
                        alert("Please Select IFSC Code.");
                        flag = false;
                    }
                    else {
                        if (flag == true) {
                            $scope.SaveBranchChange();
                        }
                    }
                }
            }
            else if ($scope.applicantDetail.IsAdmissionFeePaid == true && $scope.applicantDetail.UserConfirmAfterFee == false) {
                alert("You Have Selected Not to Proceed.");
                flag = false;
            }
            else {
                if (flag == true) {
                    $scope.SaveBranchChange();
                }
            }
        } 
    }
    $scope.SaveBranchChange = function () {
            $scope.applicantDetail.SameFeeCategory = Boolean($scope.applicantDetail.SameFeeCategory);
            $http({
                method: 'POST',
                url: 'api/BranchChangeRequest/SaveBranchChange',
                data: $scope.applicantDetail,
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
                        alert(response.obj);
                        $scope.applicantDetail = {};
                        $scope.branchChange = {};
                        $scope.showDetailflag = false;
                        $scope.ShowChangeFlag = false;
                        $scope.ShowLableFlag = false;
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });        
    };
});