
app.controller('OfficeInterAffFacultyCtrl', function ($scope, $http, $filter, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {
    
    $rootScope.pageTitle = "Manage Office Of Internal Affairs";
    
    $scope.data = [];
    $scope.CentralAdmission = {};

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
    $scope.ShowModeOfApplicationFlag = function () {
        $scope.ShowModeOfUploadFlag = true;
    }
   
    $scope.ReadExcelFile = function () {
        //$scope.onSpinner();
        var fileCheck = {};
            fileCheck = document.getElementById("ngexcelfile").value;
        var allowedExtensions = /(.xlsx|.xls)$/i;

        if (fileCheck == "" ||
            fileCheck === undefined) {
            document.getElementById("ErrorMsgUploadFile").innerHTML = "Must upload the file";
            document.getElementById("SuccessMsgUploadFile").innerHTML = "";
            //$scope.offSpinner();
            return false;
            
        }
        else if (!allowedExtensions.exec(fileCheck)) {

            document.getElementById("ErrorMsgUploadFile").innerHTML = "It only accepts .xlx and .xlsx file";
            document.getElementById("SuccessMsgUploadFile").innerHTML = "";
            fileCheck.value = '';
            //$scope.offSpinner();
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
                            var customer_info = { "FacultyId": $scope.OfficeInternal.FacultyId, "AcademicYearId": $scope.OfficeInternal.AcademicYearId, "ProgrammeInstancePartTermId": $scope.OfficeInternal.ProgrammeInstancePartTermId, "AdmissionCommitteeId": $scope.OfficeInternal.AdmissionCommitteeId, "AllotmentNo": exceljson[i].AllotmentNo, "AllotmentNo": exceljson[i].AllotmentNo, "MeritNo": exceljson[i].MeritNo, "MobileNo": exceljson[i].MobileNo, "ApplicantName": exceljson[i].ApplicantName };

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
                    $scope.offSpinner();
                    alert("Please Upload Proper Excel File");
                    return false;
                   
                }

                else {

                    $scope.OfficeInternal = { FacultyId: 1, AcademicYearId: 1, ProgrammeInstancePartTermId: 1, AdmissionCommitteeId: 1 };
                    $scope.OfficeInternal.FacultyId = $scope.CentralAdmission.FacultyId;
                    $scope.OfficeInternal.AcademicYearId = $scope.CentralAdmission.AcademicYearId;
                    $scope.OfficeInternal.ProgrammeInstancePartTermId = $scope.CentralAdmission.ProgrammeInstancePartTermId;
                    $scope.OfficeInternal.AdmissionCommitteeId = $scope.CentralAdmission.AdmissionCommitteeId;
                    var customer_info = { "FacultyId": $scope.OfficeInternal.FacultyId, "AcademicYearId": $scope.OfficeInternal.AcademicYearId, "ProgrammeInstancePartTermId": $scope.OfficeInternal.ProgrammeInstancePartTermId, "AdmissionCommitteeId": $scope.OfficeInternal.AdmissionCommitteeId, "AllotmentNo": $scope.exceljson[i].AllotmentNo, "MeritNo": $scope.exceljson[i].MeritNo, "MobileNo": $scope.exceljson[i].MobileNo, "ApplicantName": $scope.exceljson[i].ApplicantName };
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
                        //$rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        alert(response.obj);
                        
                    }


                    else {
                        /* if (response.obj == 'null') {
                             $scope.msg = "Error : Something Wrong! Please Upload Your Excel In below format.";
                             alert("error");
                         }*/
                        alert(response.obj);
                      
                        $scope.IsUploadVisible = false;
                        $scope.ShowModeOfUploadFlag = false;
                        $scope.CentralAdmission = {};
                        $scope.CentralAdmission.UploadExcel = null;
                        $("#ngexcelfile").val('');
                      
                      


                    }

                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });


        }


    };

    $scope.displayManualDetails = function () {

        $scope.IsManualVisible = true;
        $scope.IsUploadVisible = false;
    };

    $scope.displayUploadExcel = function () {

        $scope.IsUploadVisible = true;
        $scope.IsManualVisible = false;
    };

      
    $scope.resetCentralAdmission = function () {
        $scope.CentralAdmission = {};
        $scope.IsManualVisible = false;
        $scope.IsUploadVisible = false;
        $scope.ShowModeOfUploadFlag = false;
      
    };
    $scope.resetCentralAdmissionManual = function () {
        $scope.CentralAdmission = {};
        $scope.IsManualVisible = false;
        $scope.IsUploadVisible = false;
        $scope.ShowModeOfUploadFlag = false;

    };
    $scope.resetCentralAdmissionExcel = function () {
        $scope.CentralAdmission = {};
        $scope.IsManualVisible = false;
        $scope.IsUploadVisible = false;
        $scope.ShowModeOfUploadFlag = false;

    };
    $scope.FacultyId = {};
    $scope.getFacultyById = function () {
        
        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGetbyId',

            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $scope.Institute = response.obj[0];
                $scope.CentralAdmission.FacultyId = $scope.Institute.Id;
                $scope.FacultyId = response.obj[0].Id;
                $scope.getMstCentralAdmissionGetByFacId();
            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Get Academic Year List*/
    $scope.getAcademicYear = function () {

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
                   
                }

            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
   
    $scope.getProgrammeListByInstIdAcadId = function () {
      
        $scope.ProgrammeList = {};
        $scope.CentralAdm = { InstituteId: $scope.Institute.InstituteId, AcademicYearId: $scope.CentralAdmission.AcademicYearId }
        $http({
            method: 'POST',
            url: 'api/MstCentralAdmission/ProgrammeListGetByInstAcadId',
            data: $scope.CentralAdm,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgrammeList = response.obj;
                
            })
            .error(function (res) {
                alert(res);
                $scope.ProgrammeList = {};
            });
    };
    $scope.getBranchListByProgId = function () {
      
        $scope.BranchList = {}; 
       // $scope.CentralAdmission = { InstituteId: $scope.Institute.InstituteId, AcademicYearId: $scope.CentralAdmission.AcademicYearId, ProgrammeId: $scope.CentralAdmission.ProgrammeId }
        $http({
            method: 'POST',
            url: 'api/MstCentralAdmission/BranchListGetByProgId',
            data: $scope.CentralAdmission,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.BranchList = response.obj;

            })
            .error(function (res) {
                alert(res);
                $scope.BranchList = {};
            });
    };


    $scope.getInstanceNameList = function () {
       
        $scope.InstanceNameList = {};
        $scope.CentralAdm = { InstituteId: $scope.Institute.InstituteId, AcademicYearId: $scope.CentralAdmission.AcademicYearId, ProgrammeId: $scope.CentralAdmission.ProgrammeId, SpecialisationId: $scope.CentralAdmission.SpecialisationId }

        $http({
            method: 'POST',
            url: 'api/MstCentralAdmission/IncProgramInstancePartTermGetbyInsIdAcadIdProgId',
            data: $scope.CentralAdm,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code == "201") {
                  
                    //alert(response.obj);
                    $scope.InstanceNameList = {};
                }
                else {

                    $scope.InstanceNameList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getMstCentralAdmissionGetByFacId = function () {
       
        $scope.CentralAdmission.FacultyId = $scope.Institute.Id;
        console.log($scope.CentralAdmission.FacultyId);
        $http({
            method: 'POST',
            url: 'api/MstCentralAdmission/MstCentralAdmissionGetByFacultyId',
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

                    $scope.CentralAdmissionFacTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj

                    });
                    $scope.getMstCentAdmissionData = response.obj;
                }

            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getMstCentralAdmissionGetByFAPPIPTId = function () {

        $scope.CentralAdmission.FacultyId = $scope.Institute.Id;
        console.log($scope.CentralAdmission.FacultyId);
        $http({
            method: 'POST',
            url: 'api/MstCentralAdmission/MstCentralAdmissionGetByFAPIPTId',
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

                    $scope.CentralAdmissionFAPPIPTTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj

                    });
                    $scope.MstCentAdmissionAddTable = response.obj;
                    for (let i = 0; i < $scope.MstCentAdmissionAddTable.length; i++) {

                        $scope.CentAdmissionAddTable = {};
                        $scope.CentAdmissionAddTable.ProgrammeName = $scope.MstCentAdmissionAddTable[i].ProgrammeName;
                        $scope.CentAdmissionAddTable.InstancePartTermName = $scope.MstCentAdmissionAddTable[i].InstancePartTermName;
                        $scope.CentAdmissionAddTable.BranchName = $scope.MstCentAdmissionAddTable[i].BranchName;
                        $scope.CentAdmissionAddTable.CommitteeName = $scope.MstCentAdmissionAddTable[i].CommitteeName;
                    }
                    $scope.IsLabelVisible = true;
                    $scope.IsTableVisibleAdd = true;
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

    

    $scope.addCentralAdmissionFaculty = function () {
       
        $scope.CentralAdmission.FacultyId = $scope.FacultyId;
      
        if ($scope.CentralAdmission.FacultyId === null || $scope.CentralAdmission.FacultyId === undefined ||
            $scope.CentralAdmission.AcademicYearId === null || $scope.CentralAdmission.AcademicYearId === undefined ||
            $scope.CentralAdmission.ProgrammeInstancePartTermId === null || $scope.CentralAdmission.ProgrammeInstancePartTermId === undefined ||
            $scope.CentralAdmission.AllotmentNo === null || $scope.CentralAdmission.AllotmentNo === undefined ||
            $scope.CentralAdmission.MeritNo === null || $scope.CentralAdmission.MeritNo === undefined ||
            $scope.CentralAdmission.AdmissionCommitteeId === null || $scope.CentralAdmission.AdmissionCommitteeId === undefined ||
            $scope.CentralAdmission.ApplicantName === null || $scope.CentralAdmission.ApplicantName === undefined ||
            $scope.CentralAdmission.MobileNo === null || $scope.CentralAdmission.MobileNo === undefined) {
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
                        $scope.getMstCentralAdmissionGetByFAPPIPTId();
                        //$scope.IsManualVisible = false;
                        //$scope.CentralAdmission = {};
                       
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Modify Department Data*/
    $scope.modifyCentralAdmissiondataFaculty = function (data) {
    
       
        $scope.showFormFlag = true;   
        $scope.CentralAdmission = data;
        if (!($scope.AcademicYearList == null && $scope.AcademicYearList == undefined)) {
            $scope.getProgrammeListByInstIdAcadId();
           
        }
        if (!($scope.getProgrammeListByInstIdAcadId == null && $scope.getProgrammeListByInstIdAcadId == undefined)) {
            $scope.getBranchListByProgId();

        }
        if (!($scope.getBranchListByProgId == null && $scope.getBranchListByProgId == undefined)) {
            $scope.getInstanceNameList();

        }     

       
    };

    /*Update CentralAdmission*/
    $scope.updateCentralAdmissionFaculty = function () {

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
                        $scope.getMstCentralAdmissionGetByFacId();
                        $scope.showFormFlag = false;

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Delete CentralAdmission*/
    $scope.deleteCentralAdmissionFaculty = function (ev, data) {

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

   
    /*Display CentralAdmission Data*/
    $scope.displayCentralAdmissionFaculty = function (data) {
        $scope.CentralAdmission = data;
    };

  
    $scope.newCentralAdmissionFacultyAdd = function () {
        $state.go('MstCentralAdmissionFacultyAdd');
    };
   

    $scope.backToFacultyList = function () {
        $state.go('MstCentralAdmissionFacultyEdit');
    };

    $scope.exportDataFaculty = function () {

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
                { columnid: 'FacultyName', title: 'Faculty Name' },            
                { columnid: 'AcademicYearCode', title: 'Academic Year Code' },
                { columnid: 'InstancePartTermName', title: 'Programme Instance Part Term Name' },
                { columnid: 'ProgrammeName', title: 'Programme Name' },
                { columnid: 'BranchName', title: 'Branch Name' },
                { columnid: 'AllotmentNo', title: 'Allotment No' },
                { columnid: 'MeritNo', title: 'Merit No' },
                { columnid: 'CommitteeName', title: 'Admission Committee Name' },
                { columnid: 'ApplicantName', title: 'Applicant Name' },
                { columnid: 'MobileNo', title: 'Mobile No' },
               
               

            


            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.getMstCentAdmissionData]);
    };

   

});
