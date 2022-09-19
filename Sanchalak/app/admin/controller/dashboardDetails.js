app.controller("dashboardDetailsCtrl", function ($scope, $window, $rootScope, $http, $cookies, $localStorage, $state, NgTableParams, $stateParams, $filter) {

    /*scroll to top Start */

    window.onscroll = function () { scrollFunction() };

    function scrollFunction() {
        if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
            $('#fixed-go-up-icon').css("display", "block");
        } else {
            $('#fixed-go-up-icon').css("display", "none");
        }
    }

    $("#fixed-go-up-icon").click(function () {
        document.body.scrollTop = 0;
        document.documentElement.scrollTop = 0;
    });

    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }
    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }



    /*scroll to top End */

    /* go back button Start */

    $scope.goBack = function () {
        $state.go('dashboardadmin');
    }
    /* go back button End */

    /* ------------------------ Live Variables Start --------------------------------------- */

    $scope.LiveAcademicYear = $stateParams.academicYear;
    $scope.Heading = $stateParams.Heading;
    $scope.CheckName = $stateParams.checkName;
    $scope.LiveStatTag = $stateParams.statTag;
    $scope.LiveInstituteName = $stateParams.facultyName;
    $scope.LiveDepartmentName = $stateParams.departmentName;
    $scope.ModifiedInstituteId = $stateParams.modifiedInstituteId;
    $scope.ModifiedDepartmentId = $stateParams.modifiedDepartmentId;
    $scope.ModifiedExamEventId = $stateParams.modifiedExamEventId;
    var LiveColor = $localStorage.color;

    if ($localStorage.DashboardDetailStateFlag == true) {
        $scope.LiveAcademicYear = $localStorage.LiveAcademicYear;
        $scope.Heading = $localStorage.Heading;
        $scope.CheckName = $localStorage.CheckName;
        $scope.LiveStatTag = $localStorage.LiveStatTag;
        $scope.LiveInstituteName = $localStorage.LiveInstituteName;
        $scope.LiveDepartmentName = $localStorage.LiveDepartmentName;
        $scope.ModifiedInstituteId = $localStorage.ModifiedInstituteId;
        $scope.ModifiedDepartmentId = $localStorage.ModifiedDepartmentId;
        $scope.LiveExamEventId = $localStorage.ModifiedExamEventId;
        $scope.LiveColor = $localStorage.Color;

    } else if ($localStorage.DashboardDetailStateFlag == null) {
        $localStorage.LiveAcademicYear = $stateParams.academicYear;
        $localStorage.Heading = $stateParams.Heading;
        $localStorage.CheckName = $stateParams.checkName;
        $localStorage.LiveStatTag = $stateParams.statTag;
        $localStorage.LiveInstituteName = $stateParams.facultyName;
        $localStorage.LiveDepartmentName = $stateParams.departmentName;
        $localStorage.ModifiedInstituteId = $stateParams.modifiedInstituteId;
        $localStorage.ModifiedDepartmentId = $stateParams.modifiedDepartmentId;
        $localStorage.ModifiedExamEventId = $stateParams.modifiedExamEventId;
        $localStorage.DashboardStateFlag = true;
        $localStorage.DashboardDetailStateFlag = true;
    }

    $scope.RoleId = $cookies.get('roleId');
    var validating = $cookies.get('token');
    if (!validating) {
        $state.go('login');
    }

    /* ------------------------ Live Variables End --------------------------------------- */



    /* setting color Start */


    document.querySelector(':root').style.setProperty('--current_color', LiveColor);

    /* setting color End */

    /* calling functions for getting data into tables start */

    $scope.FillTable = function () {

        if ($scope.LiveStatTag == 'Application') {

            $scope.GetApplicationStudentDetails = function () {

                // Function for GeneralStatistics API Calling

                $scope.onSpinner();
                $http({

                    method: 'POST',
                    url: 'api/GetStudentStatistics/MstStudentApplicationDetails',
                    data: {
                        "AcademicYear": $scope.LiveAcademicYear,
                        "ModifiedInstituteId": $scope.ModifiedInstituteId,
                        "ModifiedSubjectId": $scope.ModifiedDepartmentId,
                        "ApplicationFlag": $scope.CheckName
                    },
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        $scope.offSpinner();

                        if (response.response_code == "0") {
                            $state.go('login');

                        } else if (response.response_code != "200") {
                            $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                            console.log("data from response", $scope.GeneralStatusInfo);
                            $state.go('dashboardadmin');

                        }
                        else {
                            let Getresponse = response.obj;
                            $scope.StudentData = Getresponse[0].Studentlist;
                            $scope.GetStudentTableParams = new NgTableParams({}, { dataset: $scope.StudentData });
                            console.log("data from response", $scope.StudentData);

                        }

                    })
                    .error(function (res) {
                        console.log("error in from api");
                        $state.go('dashboardadmin');
                        $scope.offSpinner();
                        $rootScope.$broadcast('dialog', "Error", "alert", res);
                    });

            };

            //calling functions

            $scope.GetApplicationStudentDetails();

        }

        else if ($scope.LiveStatTag == 'Admission') {

            $scope.GetAdmissionStudentDetails = function () {

                // Function for GeneralStatistics API Calling

                $scope.onSpinner();
                $http({

                    method: 'POST',
                    url: 'api/GetStudentStatistics/MstStudentAdmissionDetails',
                    data: {
                        "AcademicYear": $scope.LiveAcademicYear,
                        "ModifiedInstituteId": $scope.ModifiedInstituteId,
                        "ModifiedSubjectId": $scope.ModifiedDepartmentId,
                        "AdmissionFlag": $scope.CheckName
                    },
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        $scope.offSpinner();

                        if (response.response_code == "0") {
                            $state.go('login');

                        } else if (response.response_code != "200") {
                            $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                            console.log("data from response", $scope.GeneralStatusInfo);
                            $state.go('dashboardadmin');

                        }
                        else {
                            let Getresponse = response.obj;
                            $scope.StudentData = Getresponse[0].Studentlist;
                            $scope.GetStudentTableParams = new NgTableParams({}, { dataset: $scope.StudentData });
                            console.log("data from response", $scope.StudentData);

                        }
                    })
                    .error(function (res) {
                        console.log("error in from api");
                        $state.go('dashboardadmin');
                        $scope.offSpinner();
                        $rootScope.$broadcast('dialog', "Error", "alert", res);
                    });

            };

            //calling functions

            $scope.GetAdmissionStudentDetails();
        }

        else if ($scope.LiveStatTag == 'Pre-Examination') {

            $scope.GetPreExaminationStudentDetails = function () {

                // Function for GeneralStatistics API Calling

                $scope.onSpinner();
                $http({

                    method: 'POST',
                    url: 'api/GetStudentStatistics/MstStudentPreExaminationDetails',
                    data: {
                        "AcademicYear": $scope.LiveAcademicYear,
                        "ModifiedInstituteId": $scope.ModifiedInstituteId,
                        "ModifiedSubjectId": $scope.ModifiedDepartmentId,
                        "ModifiedExamEventId": $scope.ModifiedExamEventId,
                        "PreExaminationFlag": $scope.CheckName
                    },
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        $scope.offSpinner();

                        if (response.response_code == "0") {
                            $state.go('login');

                        } else if (response.response_code != "200") {
                            $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                            console.log("data from response", $scope.GeneralStatusInfo);
                            $state.go('dashboardadmin');

                        }
                        else {
                            let Getresponse = response.obj;

                            if ($scope.CheckName == "TotalActiveStudents" || $scope.CheckName == "PaperSelected" || $scope.CheckName == "NotSelectedPaper" || $scope.CheckName == "ExamFormsGenereated" || $scope.CheckName == "ExamFormsNotGenereated" || $scope.CheckName == "TotalRepeaters") {

                                $scope.StudentData = Getresponse[0].Studentlist;
                                $scope.GetStudentTableParams = new NgTableParams({}, { dataset: $scope.StudentData });
                                console.log("data from response", $scope.StudentData);

                            }
                            else if ($scope.CheckName == "TimeTableConfigured" || $scope.CheckName == "TimeTableNotConfigured") {

                                $scope.InstituteData = Getresponse[0].TotalTimeTable;
                                $scope.GetBranchTableParams = new NgTableParams({}, { dataset: $scope.InstituteData });
                                console.log("data from response", $scope.InstituteData);

                            }
                            else if ($scope.CheckName == "TotalExamScheduled" || $scope.CheckName == "TotalExamNotScheduled") {

                                $scope.InstituteData = Getresponse[0].TotalInstituteExams;
                                $scope.GetInstituteTableParams = new NgTableParams({}, { dataset: $scope.InstituteData });
                                console.log("data from response", $scope.InstituteData);

                            } else if ($scope.CheckName == "TotalProgrammePartTermExams") {

                                $scope.ProgramData = Getresponse[0].TotalProgrammeExams;
                                $scope.GetProgramTableParams = new NgTableParams({}, { dataset: $scope.ProgramData });
                                console.log("data from response", $scope.ProgramData);

                            }

                        }
                    })
                    .error(function (res) {
                        console.log("error in from api");
                        $state.go('dashboardadmin');
                        $scope.offSpinner();
                        $rootScope.$broadcast('dialog', "Error", "alert", res);
                    });

            };

            //calling functions

            $scope.GetPreExaminationStudentDetails();
        }

    }

    $scope.studentAllDetails = function (StudentId) {
        $state.go('ApplicantsAdmissionInformationDetails',
            {
                PRN: StudentId
            });
    }

    /* calling functions for getting data into tables End */


    /* Create PDF & Excel Start */

    $scope.exportDataToExcel = function () {

        if (($scope.LiveStatTag == 'Application') || (($scope.LiveStatTag == 'Admission') && ($scope.CheckName != 'PhysicallyChallengedStudents')) || ($scope.CheckName == 'TotalActiveStudents' || $scope.CheckName == 'PaperSelected' || $scope.CheckName == 'NotSelectedPaper' || $scope.CheckName == 'ExamFormsGenereated' || $scope.CheckName == 'ExamFormsNotGenereated' || $scope.CheckName == 'TotalRepeaters')) {

            var LongDate = new Date();
            var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
            var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
            var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

            var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
            var ExcelFileName = $scope.CheckName + "StudentList" + DateWithoutDashed + time;

            var mystyle = {
                sheetid: 'StudentData',
                headers: true,
                style: 'font-size:25px;font-weight:bold;',
                caption: {
                    title: $scope.Heading + ' Students Detail list | Time Stamp: ' + DateAndTime,
                },
                columns: [
                    { columnid: 'PRN', title: 'PRN / UserId', style: 'font-size:20px;font-weight:bold;width:80;' },
                    { columnid: 'Name', title: 'Student Name', style: 'font-size:20px;font-weight:bold;width:300;' },
                    { columnid: 'Gender', title: 'Gender', style: 'font-size:20px;font-weight:bold;width:50;' },
                    { columnid: 'InstancePartTermName', title: 'Semester', style: 'font-size:20px;font-weight:bold;width:350;' },
                    { columnid: 'InstituteName', title: 'Faculty Name', style: 'font-size:20px;font-weight:bold;width:300;' },
                    { columnid: 'MobileNo', title: 'Mobile No.', style: 'font-size:20px;font-weight:bold;width:100;' },
                    { columnid: 'EmailId', title: 'EmailId', style: 'font-size:20px;font-weight:bold;width:100;' }
                ]
            }

            //Create XLS format using alasql.js file.
            alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.StudentData]);
        }

        else if ($scope.CheckName == 'PhysicallyChallengedStudents') {

            var LongDate = new Date();
            var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
            var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
            var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

            var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
            var ExcelFileName = $scope.CheckName + "StudentList" + DateWithoutDashed + time;

            var mystyle = {
                sheetid: 'StudentData',
                headers: true,
                style: 'font-size:25px;font-weight:bold;',
                caption: {
                    title: $scope.Heading + ' Students Detail list | Time Stamp: ' + DateAndTime,
                },
                columns: [
                    { columnid: 'PRN', title: 'PRN / UserId', style: 'font-size:20px;font-weight:bold;width:80;' },
                    { columnid: 'Name', title: 'Student Name', style: 'font-size:20px;font-weight:bold;width:300;' },
                    { columnid: 'Gender', title: 'Gender', style: 'font-size:20px;font-weight:bold;width:50;' },
                    { columnid: 'DisabilityPercentage', title: 'Disability Percentage', style: 'font-size:20px;font-weight:bold;width:50;' },
                    { columnid: 'InstancePartTermName', title: 'Semester', style: 'font-size:20px;font-weight:bold;width:350;' },
                    { columnid: 'InstituteName', title: 'Faculty Name', style: 'font-size:20px;font-weight:bold;width:300;' },
                    { columnid: 'MobileNo', title: 'Mobile No.', style: 'font-size:20px;font-weight:bold;width:100;' },
                    { columnid: 'EmailId', title: 'EmailId', style: 'font-size:20px;font-weight:bold;width:100;' }
                ]
            }

            //Create XLS format using alasql.js file.
            alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.StudentData]);
        }

        else if ($scope.CheckName == 'TimeTableConfigured' || $scope.CheckName == 'TimeTableNotConfigured') {

            console.log("TimeTableConfigured");
            var LongDate = new Date();
            var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
            var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
            var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

            var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
            var ExcelFileName = $scope.CheckName + "BranchWiseList" + DateWithoutDashed + time;

            var mystyle = {
                sheetid: 'BranchWiseData',
                headers: true,
                style: 'font-size:25px;font-weight:bold;',
                caption: {
                    title: $scope.Heading + ' Branch Wise | Time Stamp: ' + DateAndTime
                },
                columns: [
                    { columnid: 'InstituteName', title: 'Faculty Name', style: 'font-size:20px;font-weight:bold;width:300;' },
                    { columnid: 'BranchName', title: 'Branch Name', style: 'font-size:20px;font-weight:bold;width:300;' },
                    { columnid: 'PartTermName', title: 'Semester', style: 'font-size:20px;font-weight:bold;width:300;' },
                    { columnid: 'DisplayName', title: 'Exam Event Name', style: 'font-size:20px;font-weight:bold;width:50;' },
                    { columnid: 'InstituteEmail', title: 'Email', style: 'font-size:20px;font-weight:bold;width:250;' }
                ]
            }

            //Create XLS format using alasql.js file.
            alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.InstituteData]);
        }

        else if ($scope.CheckName == 'TotalExamScheduled' || $scope.CheckName == 'TotalExamNotScheduled') {

            console.log("TimeTableConfigured");
            var LongDate = new Date();
            var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
            var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
            var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

            var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
            var ExcelFileName = $scope.CheckName + "BranchWiseList" + DateWithoutDashed + time;

            var mystyle = {
                sheetid: 'BranchWiseData',
                headers: true,
                style: 'font-size:25px;font-weight:bold;',
                caption: {
                    title: $scope.Heading + ' Branch Wise | Time Stamp: ' + DateAndTime
                },
                columns: [
                    { columnid: 'InstituteName', title: 'Faculty Name', style: 'font-size:20px;font-weight:bold;width:300;' },
                    { columnid: 'InstituteType', title: 'Institute Type', style: 'font-size:20px;font-weight:bold;width:100;' },
                    { columnid: 'DisplayName', title: 'Exam Event Name', style: 'font-size:20px;font-weight:bold;width:50;' },
                    { columnid: 'InstituteEmail', title: 'Email', style: 'font-size:20px;font-weight:bold;width:250;' }
                ]
            }

            //Create XLS format using alasql.js file.
            alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.InstituteData]);
        }

    };

    /* Create PDF & Excel End */

});