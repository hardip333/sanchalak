app.controller('DownloadWrittenTestListCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams, $filter) {

    $rootScope.pageTitle = "Report For Written Test List";

    $scope.WrittenTestData = {};

    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }
 
    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }

    $scope.cancelWrittenTestApplicantsList = function () {
        $scope.WrittenTestData = {};
        $scope.showFormFlag = false;
    };

    //Get funcion for Faculty
    $scope.getFacultyById = function () {
        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGetbyId',
            data: $scope.WrittenTestData,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.Faculty = response.obj[0];
                $scope.WrittenTestData.FacultyId = $scope.Faculty.Id;
                //$scope.ProgPartTermGetByInstIdandYearId();
            })
            .error(function (res) {
                alert(res);
            });
    };

    //Get funcion for Academic Year
    $scope.getAcademicList = function () {

        $http({
            method: 'POST',
            url: 'api/DownloadWrittenTestList/AcademicYearGet',
            data: $scope.WrittenTestData,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.AcadList = response.obj;

                if ($localStorage.BacktoPostPage.AcademicYearId != null) {

                    $scope.ProgPartTermGetByInstIdandYearId();
                }

                $scope.WrittenTestData.AcademicYearId = $localStorage.BacktoPostPage.AcademicYearId;

            })
            .error(function (res) {
                alert(res);
            });
    };

    //Get funcion for Programmes get
    $scope.ProgPartTermGetByInstIdandYearId = function () {

        $scope.ProgPartTermByFacIdList = {};
        $scope.WrittenTestData.InstituteId = $scope.Faculty.InstituteId;
        $scope.WrittenTestData.AcademicYearId = $scope.WrittenTestData.AcademicYearId;

        $http({
            method: 'POST',
            url: 'api/DownloadWrittenTestList/ProgPartTermGetByInstIdandYearId',
            data: $scope.WrittenTestData,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code == "201") {
                    $rootScope.broadcast('dialog', "Error", "alert", response.obj);
                    $scope.ProgPartTermByFacIdList = {};
                }
                else {
                    $scope.ProgPartTermByFacIdList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    //Get funcion for Written Test Schedule Details
    $scope.WrittenTestScheduleGet = function () {

        $http({
            method: 'POST',
            url: 'api/DownloadWrittenTestList/WrittenTestScheduleGet',
            data: $scope.WrittenTestData,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.WrittenTestSchedule = response.obj[0];
                    //$scope.Venue = $scope.WrittenTestSchedule.Venue;
                    //$scope.ExamDate = $scope.WrittenTestSchedule.ExamDate;
                    //$scope.StartTimeView = $scope.WrittenTestSchedule.StartTimeView;
                    //$scope.EndTimeView = $scope.WrittenTestSchedule.EndTimeView;

                    $scope.WrittenTestScheduleTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                    if (response.obj.length == 0) {
                        $scope.checkDataExists = true;
                    }
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    }

    //Function for get Applicant list for Written Test
    $scope.getWrittenTestApplicantsList = function (FacultyName) {

       
        $scope.checkDataforTest = false;
        $scope.WrittenTestData.ProgrammeInstancePartTermId = $scope.WrittenTestData.ProgrammeInstancePartTerm.Id;
        $scope.ProgrammeNameView = $scope.WrittenTestData.ProgrammeInstancePartTerm.InstancePartTermName;
        $scope.FacultyNameView = FacultyName;

        $scope.WrittenTestListTableparam = new NgTableParams({

        },
        { dataset: [] });
        $scope.WrittenTestData.FacultyId = $scope.WrittenTestData.Id;

        if ($scope.WrittenTestData.AcademicYearId == null || $scope.WrittenTestData.AcademicYearId === undefined) {
            alert("Please select Academic Year...!");
        }
        else if ($scope.WrittenTestData.ProgrammeInstancePartTerm.Id == null || $scope.WrittenTestData.ProgrammeInstancePartTerm.Id === undefined) {
            alert("Please select Programme...!");
        }
        else {
            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/DownloadWrittenTestList/getWrittenTestApplicantsList',
                data: $scope.WrittenTestData,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code == "201") {
                        $rootScope.broadcast('dialog', "Error", "alert", response.obj);
                        $scope.WrittenTestApplicantsList = {};
                    }
                    else {
                        $scope.ResLength = response.obj.length;
                        $scope.WrittenTestListTableparam = new NgTableParams({
                            page: 1,
                            count: $scope.ResLength
                        },
                            { counts: [], dataset: response.obj });
                        $scope.offSpinner();
                        $scope.WrittenTestApplicantsList = response.obj;
                        if (response.obj.length == 0 || response.obj == 'No Record Found') {
                            $scope.checkDataforTest = true;
                        }
                        $scope.WrittenTestScheduleGet();
                    }
                })
                .error(function (res) {
                    $rootScope.broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };
  
    //Export To Excel Code for Download Written Test Report
    $scope.ExportToExcelWrittenTestReport = function () {

        alert("Please wait, Excel is being prepared...");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "WrittenTest_Applicants_List_" + DateWithoutDashed + time;

        var mystyleSecond = {
            headers: true,
            style: 'font-size:25px;font-weight:bold;',
            caption: {
                title: '<label style=font-size:15px;> Written Test Applicants List </label>' + '<br>' +
                    'Faculty Name : ' + $scope.FacultyNameView + '<br>' +
                    'Programme Name : ' + $scope.ProgrammeNameView + '<br>' 
             },
            columns: [
                { columnid: 'IndexId', title: 'Sr No.' },
                { columnid: 'EntranceTestSeatNo', title: 'Seat No' },
                { columnid: 'ApplicationId', title: 'Application Number' },
                { columnid: 'ApplicantUserName', title: 'User Name' },
                { columnid: 'FullName', title: 'Applicant Name' },
                { columnid: 'ApplicantPhoto', title: 'Applicant Photo' },
                { columnid: 'ApplicantSignature', title: 'Applicant Signature' },


            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyleSecond, $scope.WrittenTestApplicantsList]);
    };

});