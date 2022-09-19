app.controller('DualAdmissionByAcademicCtrl', function ($scope, $http, $rootScope, $state, $cookies, Upload, $mdDialog, $localStorage, NgTableParams, httpRequestInterceptor) {
    var tokenCookie = $cookies.get('token');
    if (!tokenCookie) {
        localStorage.clear();
        $state.go('login');
    }
    $rootScope.pageTitle = "Dual Admission By Academic";
    //$scope.DualAdmission = {};

    $scope.RadioTransFlag = false;
    $scope.RadioDualFlag = false;
    $scope.DefaultRadioFlag = false;

    $scope.IsInstitute = false;
    $scope.IsSection = false;

    var today = new Date();
    var date = today.getDate();
    var month = today.getMonth() + 1;
    var year = today.getFullYear();

    $scope.expand_row = function (id) {
        let element = document.getElementById('expand' + id).classList
        if (element.contains("collapse")) {
            document.getElementById("first_col" + id).innerHTML = "-"
            element.remove("collapse")
        } else {
            document.getElementById("first_col" + id).innerHTML = "+"
            element.add("collapse")
        }
    }

    
    //Function for Find data using PRN
    $scope.DualAdmissionSearchByPRN = function (data) {
        $scope.ShowApplicantFlag = false;
        $scope.SubmitDualflag = false;
        $scope.MsgFlag = false;
        $scope.HideFlag = true;

        if ($scope.DualAdmission.PRN == null || $scope.DualAdmission.PRN === undefined) {
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Kindly Enter PRN")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
            $scope.ShowApplicantFlag = false;
            $scope.SubmitDualflag = false;
            $scope.MsgFlag = false;
            $scope.HideFlag = true;
        }
        else {
            $http({
                method: 'POST',
                url: 'api/DualAdmissionByAcademic/DualAdmissionSearchByPRN',
                data: $scope.DualAdmission,
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
                        $scope.DualAdmission = {};
                        $scope.MsgFlag = false;
                        $scope.HideFlag = true;
                    }
                    else {
                        $scope.PRNSerachData = response.obj;
                        $scope.ShowApplicantFlag = true;
                        $scope.MsgFlag = true;
                        if (response.obj[0].Message1 == 'Admitted in one Programme.') {
                            $scope.SubmitDualflag = true;
                            $scope.MsgFlag = false;
                            $scope.HideFlag = true;
                            $scope.getProgrammeListforDualAdm();
                        }
                    }

                })
                .error(function (res) {
                    $rootScope.broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    }

    //Function for get Programme List
    $scope.getProgrammeListforDualAdm = function () {
        $scope.HideFlag = true;
        $http({
            method: 'POST',
            url: 'api/DualAdmissionByAcademic/GetProgrammeListforDualAdm',
            data: { PRN: $scope.DualAdmission.PRN },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgrammeListforDualAdm = response.obj;
              
                if (response.obj == 'No Record Found') {
                    $scope.HideFlag = false;
                }

                if ($cookies.get("typePrefix") == 'INS') {
                    $scope.IsInstitute = true;
                    $scope.IsSection = false;
                }
                else if ($cookies.get("typePrefix") == 'SEC') {
                    $scope.IsSection = true;
                    $scope.IsInstitute = false;
                }
                else {
                    $scope.IsInstitute = false;
                    $scope.IsSection = false;
                }
            })
            .error(function (res) {
                alert(res);
            });
    };

    //Genric Config for file size-Dynamic Validation
    $scope.byId = function (data) {
        return $http({
            method: 'POST',
            url: 'api/DualAdmissionByAcademic/GenericConfigurationGetById',
            data: { Id: data },
            headers: { "Content-Type": 'application/json' }
        })
    };

    //Function for Attach Admission Document of Student
    $scope.UploadDualAdmissionDoc = function ($files) {

        if (($scope.IsSection == true) && ($scope.DualAdmission.TransorDualAdmission == null || $scope.DualAdmission.TransorDualAdmission == "" || $scope.DualAdmission.TransorDualAdmission == undefined)) {
            alert("Please select Admission Mode..!");
        }
        else {
            $scope.SelectedFiles = $files;

            var fileExtension = '.' + $scope.SelectedFiles[0].name.split('.').pop();
            //var name = $scope.SelectedFiles[0].name.split('.').slice(0, -1);
            $scope.DualAdmissionDoc = {};

            if ($scope.DualAdmission.TransorDualAdmission == 'TransferAdmission') {
                $scope.DualAdmissionDoc = $scope.DualAdmission.PRN + "_" + "Transfer" + "_" + "Admission" + "_" + "Doc" + "_" + date + "_" + month + "_" + year + fileExtension;
            }
            else if ($scope.DualAdmission.TransorDualAdmission == 'DualAdmission') {
                $scope.DualAdmissionDoc = $scope.DualAdmission.PRN + "_" + "Dual" + "_" + "Admission" + "_" + "Doc" + "_" + date + "_" + month + "_" + year + fileExtension;
            }
            else {
                $scope.DualAdmissionDoc = $scope.DualAdmission.PRN + "_" + "Transfer" + "_" + "Admission" + "_" + "Doc" + "_" + date + "_" + month + "_" + year + fileExtension;
            }
           
            $localStorage.DualAdmissionDoc = $scope.DualAdmissionDoc;

            $cookies.put("DualAdmissionDoc", $scope.DualAdmissionDoc);



            if ($scope.SelectedFiles && $scope.SelectedFiles.length) {

                var fileCheck = document.getElementById("file1").value;

                //var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.bmp)$/i;
                var allowedExtensions = /(\.jpg|\.jpeg)$/i;
                const fi = document.getElementById("file1");

                if (!allowedExtensions.exec(fileCheck)) {

                    document.getElementById("ErrorMsgDualDocImg").innerHTML = "It only accepts Jpeg file";
                    document.getElementById("SuccessMsgDualDocImg").innerHTML = "";
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
                                document.getElementById("ErrorMsgDualDocImg").innerHTML = "Please select a file below 512KB";
                                document.getElementById("SuccessMsgDualDocImg").innerHTML = "";
                                return false;
                            }
                            else {
                                Upload.upload({
                                    url: 'api/DualAdmissionByAcademic/UploadDualAdmissionDoc',
                                    data: {
                                        files: $scope.SelectedFiles
                                    }
                                }).then(function (response) {

                                    if (response.data.response_code == "200") {

                                        $scope.Result = response.data;
                                        document.getElementById("ErrorMsgDualDocImg").innerHTML = "";
                                        document.getElementById("SuccessMsgDualDocImg").innerHTML = "File Uploaded Successfully.";
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
        }
    };

    //Function for Radio button list
    $scope.CheckAdmissionValue = function () {
        if ($scope.DualAdmission.TransorDualAdmission == 'TransferAdmission' && $scope.IsSection == true) {
            $scope.RadioTransFlag = true;
            $scope.RadioDualFlag = false;
        }
        else if ($scope.DualAdmission.TransorDualAdmission == 'DualAdmission' && $scope.IsSection == true) {
            $scope.RadioDualFlag = true;
            $scope.RadioTransFlag = false;
        }
    };

    $scope.setId = function () {
        $scope.DualAdmission.Id = $scope.DualAdmission.InstancePartTermId.Id;
        $scope.DualAdmission.InstancePartTermName = $scope.DualAdmission.InstancePartTermId.InstancePartTermName;
    }

    //Function for update Dual Admission Data
    $scope.DualAdmissionUpdate = function (ev) {

        var DualAdmissionDoc = $("#SuccessMsgDualDocImg").text();

        if (DualAdmissionDoc == null || DualAdmissionDoc == undefined || DualAdmissionDoc == "") {
            alert("Please Add Document for Dual/ Transfer Admisssion..!");
            return false;
        }
        if ($scope.DualAdmissionDoc == null || $scope.DualAdmissionDoc == undefined || $scope.DualAdmissionDoc == "") {
            $scope.DualAdmission.DualAdmissionDoc = null;
        }
        else {
            $scope.DualAdmission.DualAdmissionDoc = $scope.DualAdmissionDoc;
        }


        if (($localStorage.DualAdmissionDoc != null || $localStorage.DualAdmissionDoc != undefined || $localStorage.DualAdmissionDoc != "")) {
            $cookies.put("DualAdmissionDoc", $localStorage.DualAdmissionDoc);
        }
        $scope.DualAdmission.ApplicationId = $scope.DualAdmission.Id;

        if (($scope.IsSection == true) && ($scope.DualAdmission.TransorDualAdmission == null || $scope.DualAdmission.TransorDualAdmission == "" || $scope.DualAdmission.TransorDualAdmission == undefined)) {
            alert("Please select Admission Mode..!");
        }
        else if ($scope.DualAdmission.ApplicationId == null || $scope.DualAdmission.ApplicationId == "" || $scope.DualAdmission.ApplicationId == undefined) {
            alert("Please select Programme for Dual/ Transfer Admission..!");
        }
        else if ($scope.DualAdmission.DualAdmissionRemark == null || $scope.DualAdmission.DualAdmissionRemark == "" || $scope.DualAdmission.DualAdmissionRemark == undefined) {
            alert("Please Add Remarks for Dual/ Transfer Admission..!");
        }
        else {

            var confirm = $mdDialog.confirm()
                .title('Are you sure to give Dual Admission from ' + '[' + $scope.PRNSerachData[0].ApplicationId + ' - ' + $scope.PRNSerachData[0].InstancePartTermName + ']' + ' to ' +  '['+ $scope.DualAdmission.InstancePartTermName + ']' +' ?')
                .textContent('')
                .ariaLabel('Lucky day')
                .targetEvent(ev)
                .ok('Yes')
                .cancel('No');

            $mdDialog.show(confirm).then(function () {


                $http({
                    method: 'POST',
                    url: 'api/DualAdmissionByAcademic/DualAdmissionUpdate',
                    data: $scope.DualAdmission,
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
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
                        }
                        else {
                            if (response.obj == "TRUE") {
                                $mdDialog.show(
                                    $mdDialog.alert()
                                        .parent(angular.element(document.querySelector('#popupContainer')))
                                        .clickOutsideToClose(true)
                                        .title("Note:")
                                        .textContent("Dual Admission has been successfully completed for " + $scope.DualAdmission.ApplicationId)
                                        .ariaLabel('Alert Dialog Demo')
                                        .ok('Okay')
                                );
                            }
                            $scope.DualAdmission = {};
                            $scope.DualPRN = {};
                            $scope.ShowApplicantFlag = false;
                            $scope.SubmitDualflag = false;
                            $scope.MsgFlag = false;
                            $scope.HideFlag = true;
                        }
                    })
                    .error(function (res) {
                        $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                    });

            });
        }
    };

    //Function for update Transfer Admission Data
    $scope.TransferAdmissionUpdate = function (ev) {

        $scope.DualAdmission.OldProgrammeInstancePartTermId = $scope.PRNSerachData[0].ProgrammeInstancePartTermId;
        $scope.DualAdmission.OldApplicationId = $scope.PRNSerachData[0].ApplicationId;

        var DualAdmissionDoc = $("#SuccessMsgDualDocImg").text();

        if (DualAdmissionDoc == null || DualAdmissionDoc == undefined || DualAdmissionDoc == "") {
            alert("Please Add Document for Dual/ Transfer Admisssion..!");
            return false;
        }
        if ($scope.DualAdmissionDoc == null || $scope.DualAdmissionDoc == undefined || $scope.DualAdmissionDoc == "") {
            $scope.DualAdmission.DualAdmissionDoc = null;
        }
        else {
            $scope.DualAdmission.DualAdmissionDoc = $scope.DualAdmissionDoc;
        }


        if (($localStorage.DualAdmissionDoc != null || $localStorage.DualAdmissionDoc != undefined || $localStorage.DualAdmissionDoc != "")) {
            $cookies.put("DualAdmissionDoc", $localStorage.DualAdmissionDoc);
        }
        $scope.DualAdmission.ApplicationId = $scope.DualAdmission.Id;

        if ($scope.DualAdmission.ApplicationId == null || $scope.DualAdmission.ApplicationId == "" || $scope.DualAdmission.ApplicationId == undefined) {
            alert("Please select Programme for Dual/ Transfer Admission..!");
        }
        else if ($scope.DualAdmission.DualAdmissionRemark == null || $scope.DualAdmission.DualAdmissionRemark == "" || $scope.DualAdmission.DualAdmissionRemark == undefined) {
            alert("Please Add Remarks for Dual/ Transfer Admission..!");
        }
        else {

            var confirm = $mdDialog.confirm()
                .title('Are you sure to Transfer Admission from ' + '[' + $scope.PRNSerachData[0].ApplicationId + ' - ' + $scope.PRNSerachData[0].InstancePartTermName + ']' + ' to ' + '[' + $scope.DualAdmission.InstancePartTermName + ']' + ' ?')
                .textContent('')
                .ariaLabel('Lucky day')
                .targetEvent(ev)
                .ok('Yes')
                .cancel('No');

            $mdDialog.show(confirm).then(function () {


                $http({
                    method: 'POST',
                    url: 'api/DualAdmissionByAcademic/TransferAdmissionUpdate',
                    data: $scope.DualAdmission,
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
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
                        }
                        else {
                            if (response.obj == "TRUE") {
                                $mdDialog.show(
                                    $mdDialog.alert()
                                        .parent(angular.element(document.querySelector('#popupContainer')))
                                        .clickOutsideToClose(true)
                                        .title("Note:")
                                        .textContent("Transfer Admission has been successfully completed for " + $scope.DualAdmission.ApplicationId)
                                        .ariaLabel('Alert Dialog Demo')
                                        .ok('Okay')
                                );
                            }
                            $scope.DualAdmission = {};
                            $scope.DualPRN = {};
                            $scope.ShowApplicantFlag = false;
                            $scope.SubmitDualflag = false;
                            $scope.MsgFlag = false;
                            $scope.HideFlag = true;
                        }
                    })
                    .error(function (res) {
                        $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                    });

            });
        }
    };

});