app.controller('IncStudentReAdmissionRequestCtrl', function ($scope, $http, $rootScope, $filter, $state, $cookies, Upload, $mdDialog, $localStorage, NgTableParams) {
    //var tokenCookie = $cookies.get('token');
    //if (!tokenCookie) {
    //    localStorage.clear();
    //    $state.go('login');
    //}
    $rootScope.pageTitle = "Student ReAdmission Request";
    var today = new Date();
    var date = today.getDate();
    var month = today.getMonth() + 1;
    var year = today.getFullYear();

    $scope.SRAR = {};
    $scope.APPId = {};

    $scope.cancelReAdmissionRequest = function () {
        $scope.IsTableVisible = false;
        $scope.IsFileUploadVisible = false;
        $scope.StudentHandWrittenDocument = {};
        $scope.StuReAdmissionReq = {};
        $localStorage.StudentHandWrittenDocument = {};
        document.getElementById("ErrorMsgHandWrittenDoc").innerHTML = null;
        document.getElementById("SuccessMsgHandWrittenDoc").innerHTML = null;
        $("#file1").val("");
        $scope.ViewHRDocImageFlag = false;
    }
    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }
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
    $scope.StudentInformationGetByPRN = function () {

        if ($scope.StuReAdmissionReq.PRN == null || $scope.StuReAdmissionReq.PRN == undefined || $scope.StuReAdmissionReq.PRN == "") {
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Kindly Enter  PRN")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );

            $scope.IsTableVisible = false;
            $scope.IsFileUploadVisible = false;
        }
        else {
            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/IncStudentReAdmissionRequest/StuedentInformationGetByPRN',
                data: $scope.StuReAdmissionReq,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    if (response.response_code == "0") {
                        $state.go('login');
                    }
                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        $scope.offSpinner();
                        alert(response.obj);
                        $scope.IsTableVisible = false;
                        $scope.IsFileUploadVisible = false;

                    }

                    else {
                        $scope.offSpinner();
                        $scope.StudentReAdmissionRequest = response.obj;
                        $scope.StudentReAdmissionRequestTableParams = new NgTableParams({
                        }, {
                            dataset: response.obj
                        })
                        $scope.IsTableVisible = true;
                        $scope.IsFileUploadVisible = false;

                      

                    }

                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.ShowFileUpload = function (data) {
        $scope.APPId = data.ApplicationId;
        $scope.StuReAdmissionReq.PRN = data.PRN;
        $scope.StuReAdmissionReq.ApplicationId = data.ApplicationId;
        $scope.StuReAdmissionReq.ProgrammeInstanceId = data.ProgrammeInstanceId;
        $scope.IsFileUploadVisible = true;
    }

    $scope.AddUpdateIncStudentReAdmissionRequest = function (ev) {


        if (($localStorage.StudentHandWrittenDocument != null || $localStorage.StudentHandWrittenDocument != undefined || $localStorage.StudentHandWrittenDocument != "")) {
            $cookies.put("HandWrittenDocument", $localStorage.StudentHandWrittenDocument);
        }

        var StudentHandWrittenDocument = $("#SuccessMsgHandWrittenDoc").text();
       
        if ((StudentHandWrittenDocument == null || StudentHandWrittenDocument == undefined || StudentHandWrittenDocument == "")) {
            alert("File Not Uploaded Or File is Not in Proper Format");
            return false;
        }

        if ($scope.StudentHandWrittenDocument == null || $scope.StudentHandWrittenDocument == undefined || $scope.StudentHandWrittenDocument == "") {
            $scope.StuReAdmissionReq.StudentHandWrittenDocument = null;
        }
        else if ($scope.StudentHandWrittenDocument == "[object Object]") {
            $scope.StuReAdmissionReq.StudentHandWrittenDocument = null;
        }
        else {
            $scope.StuReAdmissionReq.StudentHandWrittenDocument = $scope.StudentHandWrittenDocument;
        }
        if (($scope.StuReAdmissionReq.StudentHandWrittenDocument == null || $scope.StuReAdmissionReq.StudentHandWrittenDocument == "" || $scope.StuReAdmissionReq.StudentHandWrittenDocument === undefined)) {
            alert("Please Upload Hand Written Document Image..!");
            return false;
        }
        else if ($scope.StuReAdmissionReq.FacultyRemark == null || $scope.StuReAdmissionReq.FacultyRemark == "" || $scope.StuReAdmissionReq.FacultyRemark === undefined){
            alert("Please Add Faculty Remarks..!");
            return false;
        
        }
        else if ($scope.StuReAdmissionReq.chkDeclaration == null || $scope.StuReAdmissionReq.chkDeclaration == "" || $scope.StuReAdmissionReq.chkDeclaration === undefined) {
            alert("Please Confirm the Declaration Check Box..!");
            return false;
        }

      
        else {
            var confirm = $mdDialog.confirm()
                .title('Would you like to Cancel?You Wont be able to Change Later!')
                .textContent('')
                .ariaLabel('Lucky day')
                .targetEvent(ev)
                .ok('Yes')
                .cancel('No');
            $mdDialog.show(confirm).then(function () {
                $scope.onSpinner();
                $http({
                    method: 'POST',
                    url: 'api/IncStudentReAdmissionRequest/IncStudentReAdmissionRequestAddUpdate',
                    data: $scope.StuReAdmissionReq,
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        if (response.response_code == "0") {
                            $state.go('login');
                        }
                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
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
                            $scope.offSpinner();
                            $scope.StuReAdmissionReq = {};
                            $scope.IsTableVisible = false;
                            $scope.IsFileUploadVisible = false;
                            $scope.StudentHandWrittenDocument = {};
                            $localStorage.StudentHandWrittenDocument = {};
                            document.getElementById("ErrorMsgHandWrittenDoc").innerHTML = null;
                            document.getElementById("SuccessMsgHandWrittenDoc").innerHTML = null;
                            $("#file1").val("");
                            $scope.ViewHRDocImageFlag = false;

                        }

                        else {
                            alert(response.obj);
                            $scope.offSpinner();
                            $scope.StuReAdmissionReq = {};
                            $scope.IsTableVisible = false;
                            $scope.IsFileUploadVisible = false;
                            $scope.StudentHandWrittenDocument = {};
                            $localStorage.StudentHandWrittenDocument = {};
                            document.getElementById("ErrorMsgHandWrittenDoc").innerHTML = null;
                            document.getElementById("SuccessMsgHandWrittenDoc").innerHTML = null;
                            $("#file1").val("");
                            $scope.ViewHRDocImageFlag = false;
                        }

                    })
                    .error(function (res) {
                        $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                    });
            }, function () {
                $scope.status = 'You decided to keep your debt.';
            });
        }

    };



    //Genric Config for file size-Dynamic Validation
    $scope.byId = function (data) {
        return $http({
            method: 'POST',
            url: 'api/IncStudentReAdmissionRequest/GenericConfigurationGetById',
            data: { Id: data },
            headers: { "Content-Type": 'application/json' }
        })
    };

    

    //Uploading StudentHandwrittenApplication Method Starts
    $scope.UploadHandWrittenDocument = function ($files) {
        
        $scope.SelectedFiles = $files;
        var fileExtension = '.' + $scope.SelectedFiles[0].name.split('.').pop();
        //var name = $scope.SelectedFiles[0].name.split('.').slice(0, -1);
        $scope.StudentHandWrittenDocument = $scope.APPId+"_"+"HandWrittenDocument" + "_" + date + "_" + month + "_" + year + fileExtension;
        $localStorage.StudentHandWrittenDocument = $scope.StudentHandWrittenDocument;
        $cookies.put("HandWrittenDocument", $scope.StudentHandWrittenDocument);

        if ($scope.SelectedFiles && $scope.SelectedFiles.length) {

            var fileCheck = document.getElementById("file1").value;

            var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.bmp)$/i;
            const fi = document.getElementById("file1");


            if (!allowedExtensions.exec(fileCheck)) {

                document.getElementById("ErrorMsgHandWrittenDoc").innerHTML = "It only accepts Jpeg file";
                document.getElementById("SuccessMsgHandWrittenDoc").innerHTML = "";
                fileCheck.value = '';
                $("#file1").val("");
                $scope.ViewHRDocImageFlag = false;
                return false;
            }

            if (fi.files.length > 0) {
                var cnt = fi.files.length;
                $scope.byId(1).then(function (response) {
                    $scope.minVal = response.data.obj[0].Value;
                    $scope.byId(5).then(function (response) {
                        $scope.maxVal = response.data.obj[0].Value;
                        for (i = 0; i < cnt; i++) {
                            const fsize = fi.files.item(i).size;
                            const file = Math.round((fsize / 1024));
                            if (file >= $scope.maxVal) {
                                document.getElementById("ErrorMsgHandWrittenDoc").innerHTML = "Please select a file below 512KB";
                                document.getElementById("SuccessMsgHandWrittenDoc").innerHTML = "";
                                $scope.ViewHRDocImageFlag = false;
                                return false;

                            }
                            else {

                                Upload.upload({
                                    url: 'api/IncStudentReAdmissionRequest/UploadStudentHandWrittenDocument',
                                    data: {
                                        files: $scope.SelectedFiles
                                    }
                                }).then(function (response) {

                                    if (response.data.response_code == "200") {
                                        $scope.SRAR.HandWrittenDocument = response.data.obj;
                                        document.getElementById("ErrorMsgHandWrittenDoc").innerHTML = "";
                                        document.getElementById("SuccessMsgHandWrittenDoc").innerHTML = "File Uploaded Successfully..!";
                                        $scope.ViewHRDocImageFlag = true;
                                    }
                                    else if (response.data.response_code == "0") {

                                        alert(response.data.obj);
                                        $state.go('login');
                                    }
                                    else {
                                        $scope.StudentHandWrittenDocument = undefined;
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

                }).catch(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });



            }



        }

    };
        //Uploading StudentHandwrittenApplication Method Ends




   
    

  

   
  
    

});