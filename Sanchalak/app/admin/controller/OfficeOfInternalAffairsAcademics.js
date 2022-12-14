app.controller('OfficeInterAffAcademicCtrl', function ($scope, $http, Upload, $filter, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Office Of Internal Affairs";

    $scope.IsUploadAcademicVisible = false;

    $scope.data = [];
    var today = new Date();
    var date = today.getDate();
    var month = today.getMonth() + 1;
    var year = today.getFullYear();

    $scope.UploadImageDocument = function ($files) {



        $scope.SelectedFiles = $files;
        var fileExtension = '.' + $scope.SelectedFiles[0].name.split('.').pop();
        // var name = $scope.SelectedFiles[0].name.split('.').slice(0, -1);
        $scope.AddExcelFileDoc = "ImportExcelFile" + "_" + date + "_" + month + "_" + year + fileExtension;
        $cookies.put("ImportExcelFile", $scope.AddExcelFileDoc);


        if ($scope.SelectedFiles && $scope.SelectedFiles.length) {

            var fileCheck = document.getElementById("ngexcelfile").value;

            var allowedExtensions = /(.xlsx|.xls)$/i;
            const fi = document.getElementById("ngexcelfile");


            if (!allowedExtensions.exec(fileCheck)) {

                document.getElementById("ErrorMsgUploadFile").innerHTML = "It only accepts xls.xlsx file";
                document.getElementById("SuccessMsgUploadFile").innerHTML = "";
                fileCheck.value = '';
                return false;
            }

            Upload.upload({
                url: 'api/MstCentralAdmission/UploadFacultyExcelDocument',
                data: {
                    files: $scope.SelectedFiles


                }
            }).then(function (response) {
                if (response.data.response_code == "200") {



                    $scope.Result = response.data;
                    document.getElementById("ErrorMsgUploadFile").innerHTML = "";
                    document.getElementById("SuccessMsgUploadFile").innerHTML = "File Uploaded Successfully..Click Add Button to Save the data..!";
                }
                else {

                    $scope.AddExcelFileDoc = undefined;
                    $("#ngexcelfile").val("");
                    alert("File Upload Failed!");
                    return false;
                }
                $scope.Result = response.data;



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
    /*Get Faculty List*/
    $scope.getFacultyList = function () {
       $scope.FacultyList = {};
        $http({
            method: 'POST',
            url: 'api/MstCentralAdmission/MstFacultyGetForDropDown',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.FacultyList = response.obj;
               

            })
            .error(function (res) {
                alert(res);
                $scope.FacultyList = {};
                console.log("$scope.FacultyList");
                console.log($scope.FacultyList);

            });
    };


    $scope.ReadExcelFile = function () {

        var fileCheck = {};
        fileCheck = document.getElementById("ngexcelfile").value;
        var allowedExtensions = /(.xlsx|.xls)$/i;

        if (fileCheck == "" ||
            fileCheck === undefined) {
            document.getElementById("ErrorMsgUploadFile").innerHTML = "Must upload the file";
            document.getElementById("SuccessMsgUploadFile").innerHTML = "";
            return false;
        }
        else if (!allowedExtensions.exec(fileCheck)) {

            document.getElementById("ErrorMsgUploadFile").innerHTML = "It only accepts .xlx and .xlsx file";
            document.getElementById("SuccessMsgUploadFile").innerHTML = "";
            fileCheck.value = '';

            return false;
        }

        else {
            document.getElementById("ErrorMsgUploadFile").innerHTML = "";
            /*Checks whether the file is a valid excel file*/
            var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.xlsx|.xls)$/;
            var xlsxflag = false; /*Flag for checking whether excel is .xls format or .xlsx format*/

            if ($("#ngexcelfile").val().toLowerCase().indexOf(".xlsx") > 0) {
                xlsxflag = true;
            }
            var reader = new FileReader();
            reader.onload = function (e) {
                var data = {};
                data = e.target.result;

                if (xlsxflag) {
                    var workbook = XLSX.read(data, { type: 'binary' });
                }
                else {
                    var workbook = XLS.read(data, { type: 'binary' });
                }

                var sheet_name_list = workbook.SheetNames;
                var cnt = 0;
                sheet_name_list.forEach(function (y) { /*Iterate through all sheets*/

                    if (xlsxflag) {
                        var exceljson = XLSX.utils.sheet_to_json(workbook.Sheets[y]);
                    }
                    else {
                        var exceljson = XLS.utils.sheet_to_row_object_array(workbook.Sheets[y]);
                    }

                    if (exceljson.length > 0) {
                        $scope.data = [];
                        for (var i = 0; i < exceljson.length; i++) {
                            $scope.OfficeInternal = { FacultyId: 1, AcademicYearId: 1, ProgrammeInstancePartTermId: 1, AdmissionCommitteeId: 1 };
                            $scope.OfficeInternal.FacultyId = $scope.CentralAdmission.FacultyId;
                            $scope.OfficeInternal.AcademicYearId = $scope.CentralAdmission.AcademicYearId;
                            $scope.OfficeInternal.ProgrammeInstancePartTermId = $scope.CentralAdmission.ProgrammeInstancePartTermId;
                            $scope.OfficeInternal.AdmissionCommitteeId = $scope.CentralAdmission.AdmissionCommitteeId;
                            var customer_info = { "FacultyId": $scope.OfficeInternal.FacultyId, "AcademicYearId": $scope.OfficeInternal.AcademicYearId, "ProgrammeInstancePartTermId": $scope.OfficeInternal.ProgrammeInstancePartTermId, "AdmissionCommitteeId": $scope.OfficeInternal.AdmissionCommitteeId, "AllotmentNo": exceljson[i].AllotmentNo, "AllotmentNo": exceljson[i].AllotmentNo, "MeritNo": exceljson[i].MeritNo, "MobileNo": exceljson[i].MobileNo };

                            $scope.data.push(customer_info);
                            // $scope.data.push($scope.PaperInt);
                            $scope.$apply();
                            //console.log($scope.data);

                        }
                    }
                });
                $scope.save($scope.data);
            }
            if (xlsxflag) {
                reader.readAsArrayBuffer($("#ngexcelfile")[0].files[0]);
            }
            else {
                reader.readAsBinaryString($("#ngexcelfile")[0].files[0]);
            }



        }

    };

    $scope.exceljson = {};
    // Save excel data to our database
    $scope.save = function (data) {

        if ($scope.CentralAdmission.FacultyId === null || $scope.CentralAdmission.FacultyId === undefined ||
            $scope.CentralAdmission.AcademicYearId === null || $scope.CentralAdmission.AcademicYearId === undefined ||
            $scope.CentralAdmission.ProgrammeInstancePartTermId === null || $scope.CentralAdmission.ProgrammeInstancePartTermId === undefined ||
            $scope.CentralAdmission.AdmissionCommitteeId === null || $scope.CentralAdmission.AdmissionCommitteeId === undefined) {

            alert("please check all fields !!!");
            fileCheck = {};
        }

        else {

            var params = [];

            $scope.exceljson = data;
            for (var i = 0; i < $scope.exceljson.length; i++) {
                $scope.Allot = $scope.exceljson[i].AllotmentNo;
                $scope.MeritNo = $scope.exceljson[i].MeritNo;

                if ($scope.exceljson[i].AllotmentNo === undefined || $scope.exceljson[i].AllotmentNo === null ||
                    $scope.exceljson[i].MeritNo === undefined || $scope.exceljson[i].MeritNo === null) {

                    //alert("Please Upload Proper Excel File");
                    $("#ngexcelfile").val("");
                    document.getElementById("ErrorMsgUploadFile").innerHTML = "Please Upload Proper Excel File";
                    document.getElementById("SuccessMsgUploadFile").innerHTML = "";

                    return false;

                }
           

                else {

                    $scope.OfficeInternal = { FacultyId: 1, AcademicYearId: 1, ProgrammeInstancePartTermId: 1, AdmissionCommitteeId: 1 };
                    $scope.OfficeInternal.FacultyId = $scope.CentralAdmission.FacultyId;
                    $scope.OfficeInternal.AcademicYearId = $scope.CentralAdmission.AcademicYearId;
                    $scope.OfficeInternal.ProgrammeInstancePartTermId = $scope.CentralAdmission.ProgrammeInstancePartTermId;
                    $scope.OfficeInternal.AdmissionCommitteeId = $scope.CentralAdmission.AdmissionCommitteeId;
                    var customer_info = { "FacultyId": $scope.OfficeInternal.FacultyId, "AcademicYearId": $scope.OfficeInternal.AcademicYearId, "ProgrammeInstancePartTermId": $scope.OfficeInternal.ProgrammeInstancePartTermId, "AdmissionCommitteeId": $scope.OfficeInternal.AdmissionCommitteeId, "AllotmentNo": $scope.exceljson[i].AllotmentNo, "MeritNo": $scope.exceljson[i].MeritNo, "MobileNo": $scope.exceljson[i].MobileNo };
                    params.push(customer_info);

                }
            }


            $http({
                method: 'POST',
                url: 'api/MstCentralAdmission/MstCenterAdmissionAddExcel',
                data: params,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }


                    else {
                        /* if (response.obj == 'null') {
                             $scope.msg = "Error : Something Wrong! Please Upload Your Excel In below format.";
                             alert("error");
                         }*/
                        alert(response.obj);
                        
                      

                        $scope.IsUploadVisible = false;
                        $scope.IsUploadAcademicVisible = false;
                        $scope.CentralAdmission = {};
                        $scope.CentralAdmission.UploadExcel = null;
                        $("#ngexcelfile").val('');
                        $scope.getMstCentralAdmission();


                    }

                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });


        }








    }

   
    $scope.displayAcademicManualDetails = function () {

        $scope.IsManualAcademicVisible = true;
        $scope.IsUploadAcademicVisible = false;
    };

    $scope.displayAcademicUploadExcel = function () {

        $scope.IsUploadAcademicVisible = true;
        $scope.IsManualAcademicVisible = false;
    };




    $scope.CentralAdmission = {};

    $scope.resetCentralAdmission = function () {
        $scope.CentralAdmission = {};
        $scope.IsManualVisible = false;
        $scope.IsUploadVisible = false;
        $scope.IsUploadAcademicVisible = false;
        $scope.IsManualAcademicVisible = false;
    };

   

    $scope.getMstCentralAdmission = function () {
        var data = new Object();
        $http({
            method: 'POST',
            url: 'api/MstCentralAdmission/MstCentralAdmissionGet',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {

                    $scope.CentralCommitteeTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj

                    });
                    $scope.MstCentAdmissionAcademicData = response.obj;
                }

            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };


   
 

    /*Get Academic Year List*/
    $scope.getAcademicYear = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstCentralAdmission/AcademicYearGetForDropDown',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {

                    $scope.AcademicYearList = response.obj;

                    console.log("$scope.AcademicYearList");
                    console.log($scope.AcademicYearList);
                }

            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };


    /*Get IncProgrammeInstancePartTerm List*/
    $scope.getIncProgrammeInstancePartTermByFacId = function () {
     
        $http({
            method: 'POST',
            url: 'api/MstCentralAdmission/IncProgramInstancePartTermGetByFacultyId',
            data: $scope.CentralAdmission,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    
                   
                }
                else {

                    $scope.IncProgInstanceListByFaculty = response.obj;
                }

            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                
            });
    };


    $scope.getMstAdmissionCommittee = function () {
        var data = new Object();
        $http({
            method: 'POST',
            url: 'api/MstCentralAdmission/MstAdmissionCommitteeGet',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {

                    $scope.MstAdmissionCommitteeList = response.obj;
                }

            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Add CentralAdmission*/
    $scope.addCentralAdmission = function () {

        if ($scope.CentralAdmission.FacultyId === null || $scope.CentralAdmission.FacultyId === undefined ||
            $scope.CentralAdmission.AcademicYearId === null || $scope.CentralAdmission.AcademicYearId === undefined ||
            $scope.CentralAdmission.ProgrammeInstancePartTermId === null || $scope.CentralAdmission.ProgrammeInstancePartTermId === undefined ||
            $scope.CentralAdmission.AllotmentNo === null || $scope.CentralAdmission.AllotmentNo === undefined ||
            $scope.CentralAdmission.MeritNo === null || $scope.CentralAdmission.MeritNo === undefined ||
            $scope.CentralAdmission.AdmissionCommitteeId === null || $scope.CentralAdmission.AdmissionCommitteeId === undefined) {
            alert("please check all fields !!!");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstCentralAdmission/MstCentralAdmissionAdd',
                data: $scope.CentralAdmission,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.IsManualAcademicVisible = false;
                        $scope.getMstCentralAdmission();
                        $scope.CentralAdmission = {};
                       
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                    $scope.msg = "Error : Something Wrong";
                });
        }
    };

    

    /*Modify Department Data*/
    $scope.modifyCentralAdmissiondata = function (data) {
       
        $scope.showFormFlag = true;
        $scope.CentralAdmission = data;

      
       /* if (!($scope.getFacultyList == null && $scope.getFacultyList == undefined)) { $scope.getIncProgrammeInstancePartTermByFacId(); }*/
        $scope.getIncProgrammeInstancePartTermByFacId();
      
        console.log($scope.CentralAdmission);
        $(window).scrollTop(0);
    };

    /*Update CentralAdmission*/
    $scope.updateCentralAdmission = function () {

        if ($scope.CentralAdmission.FacultyId === null || $scope.CentralAdmission.FacultyId === undefined ||
            $scope.CentralAdmission.AcademicYearId === null || $scope.CentralAdmission.AcademicYearId === undefined ||
            $scope.CentralAdmission.ProgrammeInstancePartTermId === null || $scope.CentralAdmission.ProgrammeInstancePartTermId === undefined ||
            $scope.CentralAdmission.AllotmentNo === null || $scope.CentralAdmission.AllotmentNo === undefined ||
            $scope.CentralAdmission.MeritNo === null || $scope.CentralAdmission.MeritNo === undefined ||
            $scope.CentralAdmission.AdmissionCommitteeId === null || $scope.CentralAdmission.AdmissionCommitteeId === undefined) {

            alert("please check all fields !!!");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstCentralAdmission/MstCentralAdmissionEdit',
                data: $scope.CentralAdmission,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.CentralAdmission = {};
                        $scope.getMstCentralAdmission();
                        $scope.showFormFlag = false;

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Delete CentralAdmission*/
    $scope.deleteCentralAdmission = function (ev, data) {

        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.CentralAdmission = data;
            $http({
                method: 'POST',
                url: 'api/MstCentralAdmission/MstCentralAdmissionDelete',
                data: $scope.CentralAdmission,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {

                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.getMstCentralAdmission();
                        $scope.getMstCentralAdmissionGetByFacId();
                    }
                })
                .error(function (res) {
                    alert(response.obj);
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
            // };
        }, function () {
            $scope.status = 'You decided not to delete your data.';
            alert($scope.status);
        });
    };

    /*Active Enable CentralAdmission*/
    $scope.showCentralAdmission = function (data) {

        $scope.newCentralAdmission = data;

        $http({
            method: 'POST',
            url: 'api/MstCentralAdmission/MstCentralAdmissionIsActive',
            data: $scope.newCentralAdmission,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.getMstCentralAdmission();

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    /*Active Disable Department*/
    $scope.hideCentralAdmission = function (data) {

        $scope.newCentralAdmission = data;

        $http({
            method: 'POST',
            url: 'api/MstCentralAdmission/MstCentralAdmissionIsSuspended',
            data: $scope.newCentralAdmission,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.getMstCentralAdmission();

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    /*Display CentralAdmission Data*/
    $scope.displayCentralAdmission = function (data) {
        $scope.CentralAdmission = data;
    };

    /*Add New CentralAdmission*/
    $scope.newCentralAdmissionAdd = function () {
        $state.go('MstCentralAdmissionAdd');

    };

   
    /*Back to Edit Page of CentralAdmission*/
    $scope.backToList = function () {
        $state.go('MstCentralAdmissionEdit');
    };

    


    $scope.exportDataAcademic = function () {

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();
        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "CentralAdmissionDetails" + DateWithoutDashed + time;
        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title: 'CentralAdmissionDetails| Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'FacultyName', title: 'FacultyName' },
                { columnid: 'AcademicYearCode', title: 'AcademicYearCode' },
                { columnid: 'InstancePartTermName', title: 'ProgrammeInstancePartTermName' },
                { columnid: 'CommitteeName', title: 'AdmissionCommitteeName' },
                { columnid: 'AllotmentNo', title: 'AllotmentNo' },
                { columnid: 'MeritNo', title: 'MeritNo' },
                { columnid: 'ApplicantName', title: 'ApplicantName' },
                { columnid: 'MobileNo', title: 'MobileNo' },



            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.MstCentAdmissionAcademicData]);
    };

});
