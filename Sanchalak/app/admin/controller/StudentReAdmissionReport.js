app.controller('StudentReAdmissionReportController', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Student Re-Admission Report";

    $scope.cardTitle = "Manage Student Re-Admission Report";

  
    $scope.StudentReAdmissionTableFlag = false;
    $scope.StuReAdmReport = {};

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

    $scope.CancelStudentReAdmissionReport = function () {
      
        $scope.StuReAdmReport = {};
        $scope.StudentReAdmissionTableFlag = false;
        $scope.IsExcelButton = false;
        $scope.IsLabelVisible = false;
        $scope.NoRecLabel = false;
    }
    $scope.GetFacultyById = function () {

        $http({
            method: 'POST',
            url: 'api/StudentReAdmissionReport/FacultyListGetByUserId',

            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
               
             
                $scope.FacultyList = response.obj; 
               


            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.GetInstituteListByFacId = function () {

        $scope.InstituteList = {};
       
        $http({
            method: 'POST',
            url: 'api/StudentReAdmissionReport/InstituteListGetByFacultyId',
            data: $scope.StuReAdmReport,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.InstituteList = response.obj;
            })
            .error(function (res) {
                alert(res);
                $scope.InstituteList = {};
            });
    };
    
    $scope.GetAcademicYearList = function () {
        
        $scope.AcademicYearList = {};
        $http({
            method: 'POST',
            url: 'api/StudentReAdmissionReport/IncAcademicYearGet',
            headers: { "Content-Type": 'application/json' }
        })

            .success(function (response) {
                $scope.AcademicYearList = response.obj;



            })
            .error(function (res) {
                $scope.AcademicYearList = {};
            });
    };

    $scope.GetProgrammeListByInstIdAcadId = function () {

        $scope.ProgrammeList = {};
          $http({
            method: 'POST',
            url: 'api/StudentReAdmissionReport/ProgrammeListGetByInstituteAcademicId',
            data: $scope.StuReAdmReport,
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

    $scope.GetInstanceNameListByInstIdAcadIdProgId = function () {
        $scope.InstanceNameList = {};
   
        $http({
            method: 'POST',
            url: 'api/StudentReAdmissionReport/IncProgramInstancePartTermGetbyInsIdAcadIdProgId',
            data: $scope.StuReAdmReport,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code == "201") {
                    $rootScope.broadcast('dialog', "Error", "alert", response.obj);
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


    $scope.GetStudentReAdmissionReportByPIPTID = function () {
        if ($scope.StuReAdmReport.AcademicYearId == null || $scope.StuReAdmReport.AcademicYearId == undefined || $scope.StuReAdmReport.AcademicYearId == ""

        ) {

            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Kindly Select Academic Year DropDown Value")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else if ($scope.StuReAdmReport.ProgrammeId == null || $scope.StuReAdmReport.ProgrammeId == undefined || $scope.StuReAdmReport.ProgrammeId == ""

        ) {

            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Kindly Select Programme DropDown Value")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else if ($scope.StuReAdmReport.ProgrammeInstancePartTermId == null || $scope.StuReAdmReport.ProgrammeInstancePartTermId == undefined || $scope.StuReAdmReport.ProgrammeInstancePartTermId == ""

        ) {

            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Kindly Select Programme Instance Part Term DropDown Value")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/StudentReAdmissionReport/StudentReAdmissionReportGetByPIPTID',
                data: $scope.StuReAdmReport,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        $scope.NoRecLabel = true;
                        $scope.IsLabelVisible = false;
                        $scope.StudentReAdmissionTableFlag = false;
                        $scope.offSpinner();
                    }

                    else {
                        $scope.offSpinner();
                        $scope.StudentReAdmissionReport = response.obj;
                        for (let i = 0; i < $scope.StudentReAdmissionReport.length; i++) {

                            $scope.ProgramLbl = {};
                            $scope.ProgramLbl.FacultyName = $scope.StudentReAdmissionReport[i].FacultyName;
                            $scope.ProgramLbl.ProgrammeName = $scope.StudentReAdmissionReport[i].ProgrammeName;
                            $scope.ProgramLbl.InstancePartTermName = $scope.StudentReAdmissionReport[i].InstancePartTermName;
                            $scope.ProgramLbl.BranchName = $scope.StudentReAdmissionReport[i].BranchName;
                        }
                        $scope.IsLabelVisible = true;
                        $scope.NoRecLabel = false;
                        $scope.StudentReAdmissionTableFlag = true;
                        $scope.StudentReAdmissionReportTableParams = new NgTableParams({
                        }, {
                            dataset: response.obj
                        });


                      




                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });

        }
    };


    $scope.ExportStudentReAdmissionReportInExcel = function () {

        $scope.StudentReAdmissionTableFlag = true;
        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();
        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "StudentReAdmissionReportDetails_" + DateWithoutDashed + time;
        var ExcelFileName = "StudentReAdmissionReportDetails_" + DateWithoutDashed + time;
        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title: 'Student Re Admission Report Details | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'ApplicationFormNum', title: 'Application Form Number' },
                { columnid: 'UserName', title: 'Student PRN' },
                { columnid: 'NameAsPerMarksheet', title: 'Student Name' },
                { columnid: 'EmailId', title: 'Email Id' },
                { columnid: 'MobileNo', title: 'Mobile No' },
                { columnid: 'FacultyRemark', title: 'Faculty Remark' },
                { columnid: 'FacultyName', title: 'Faculty Name' },
                { columnid: 'ProgrammeName', title: 'Programme Name' },
                { columnid: 'BranchName', title: 'Branch Name' },
                { columnid: 'InstancePartTermName', title: 'Instance Part Term Name' },
               


            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.StudentReAdmissionReport]);
    };





    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }


});