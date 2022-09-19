app.controller("dashboardCtrl", function ($scope, $window, $rootScope, $http, $cookies, $localStorage, $state, NgTableParams) {

    //console.log("arrived from controller")

    /* ------------------------ Current Year Set In Calender Start --------------------------------------- */

    var CurrentYear = new Date().getFullYear();
    var CurrentMonth = new Date().getMonth();
    if (CurrentMonth <= 6) {
        $scope.CurrentAcademicYear = `${CurrentYear - 1}-${CurrentYear}`;
    }
    else {
        $scope.CurrentAcademicYear = `${CurrentYear}-${CurrentYear + 1}`;
    }

    /* ------------------------ Current Year Set In Calender End --------------------------------------- */


    /* ------------------------ Live Variables Start --------------------------------------- */

    $scope.LiveEntity = "studentsPanel";
    $scope.LiveStat = "student_application";

    if ($localStorage.DashboardStateFlag == true) {
        $scope.LiveAcademicYear = $localStorage.LiveAcademicYear;
        $scope.LiveInstituteId = $localStorage.ModifiedInstituteId;
        $scope.LiveInstituteName = $localStorage.LiveInstituteName;
        $scope.LiveDepartmentId = $localStorage.ModifiedDepartmentId;
        $scope.LiveDepartmentName = $localStorage.LiveDepartmentName;
        $scope.LiveExamEventId = $localStorage.ModifiedExamEventId;
        $localStorage.DashboardDetailStateFlag = null;
        $localStorage.DashboardStateFlag = null;
    }

    else if ($localStorage.DashboardStateFlag == null) {
        $scope.LiveAcademicYear = $scope.CurrentAcademicYear;
        $scope.LiveInstituteId = 0;
        $scope.LiveInstituteName = "";
        $scope.LiveDepartmentId = 0;
        $scope.LiveDepartmentName = "";
        $scope.LiveExamEventId = 0;
        $localStorage.LiveAcademicYear = null;
        $localStorage.Heading = null;
        $localStorage.CheckName = null;
        $localStorage.LiveStatTag = null;
        $localStorage.LiveInstituteName = null;
        $localStorage.LiveDepartmentName = null;
        $localStorage.ModifiedInstituteId = null;
        $localStorage.ModifiedDepartmentId = null;
        $localStorage.ModifiedExamEventId = null;
        //$localStorage.DashboardStateFlag = null;
        //$localStorage.DashboardDetailStateFlag = null;
    }

    $scope.RoleId = $cookies.get('roleId');
    var validating = $cookies.get('token');
    if (!validating) {
        $state.go('login');
    }

    /* ------------------------ Live Variables End --------------------------------------- */


    /* ------------------------ Change Color Start --------------------------------------- */

    var GreenColor = '#17A689';
    var BlueColor = '#0055aa';
    var RedColor = '#ef3038';
    var lightOrangeColor = '#e2725b';
    var PlumBlueColor = '#2c3e50';

    var LiveColor;

    if (CurrentMonth % 3 == 0) {
        LiveColor = PlumBlueColor;
        $localStorage.color = LiveColor;

    } else if ((CurrentMonth % 2 == 0) && (CurrentMonth % 3 != 0)) {
        LiveColor = PlumBlueColor;
        //LiveColor = GreenColor;
        $localStorage.color = LiveColor;
    } else {
        LiveColor = PlumBlueColor;
        //LiveColor = BlueColor;
        $localStorage.color = LiveColor;
    }

    document.querySelector(':root').style.setProperty('--current_color', LiveColor);

    /* ------------------------ Change Color End --------------------------------------- */


    /* ------------------------ Changing CurrentYear & Institute and call methods again Start --------------------------------------- */

    /* changing year Start */
    $scope.ChangeAcademicYear = function () {
        $scope.LiveAcademicYear = document.getElementById("dropper-shadow").value;
    };

    var callAllMethods = function () {

        $scope.GeneralStatistics();

        var ElementIdFunctionMapping = {

            /* LiveStat mapping */

            // studnets stat
            student_application: $scope.StudentApplicationStat,
            student_admission: $scope.StudentAdmissionStat,
            student_pre_examination: $scope.StudentPre_ExaminationStat,
            student_examination: $scope.StudentExaminationStat,
            student_academics: $scope.StudentAcademicStat

            // teachers stat
        };

        /* Id and Function Mapping End */

        for (var ele in ElementIdFunctionMapping) {

            if (ele == $scope.LiveStat) {
                ElementIdFunctionMapping[ele]();
                //console.log(ele, ElementIdFunctionMapping[ele]);
            }

        }
    }

    $scope.$watch('LiveAcademicYear', function (newVal, oldVal) {
        if (newVal !== oldVal) {

            $scope.LiveExamEventId = 0;
            /* Id and Function Mapping Start */
            callAllMethods();
        }
    });
    /* changing year End */

    /* changing Institute Start */

    $scope.selectedFaculty = function (id, name) {
        $scope.LiveInstituteId = id;
        $scope.LiveInstituteName = name;
        $('#All-Institute-MegaMenu-Outer').css("display", "none");
        if (id) {
            callAllMethods();
        }
    }

    /* changing Institute End */

    /* changing Department Start */

    $scope.selectedDepartment = function (id, name) {
        $scope.LiveDepartmentId = id;
        $scope.LiveDepartmentName = name;
        $('#All-Institute-MegaMenu-Outer').css("display", "none");
        if (id) {
            callAllMethods();
        }
    }

    /* changing Institute End */

    /* reload page start */

    $scope.reloadPage = function () {
        
        $scope.LiveAcademicYear = $scope.CurrentAcademicYear;
        $scope.LiveInstituteId = 0;
        $scope.LiveInstituteName = "";
        $scope.LiveDepartmentId = 0;
        $scope.LiveDepartmentName = "";
        $scope.LiveExamEventId = 0;
        $localStorage.LiveAcademicYear = null;
        $localStorage.Heading = null;
        $localStorage.CheckName = null;
        $localStorage.LiveStatTag = null;
        $localStorage.LiveInstituteName = null;
        $localStorage.LiveDepartmentName = null;
        $localStorage.ModifiedInstituteId = null;
        $localStorage.ModifiedDepartmentId = null;
        $localStorage.ModifiedExamEventId = null;
        $localStorage.DashboardStateFlag = null;
        $localStorage.DashboardDetailStateFlag = null;

        callAllMethods();
    }

    /* reload page end */

    /* back button start */

    $scope.goBack = function () {

        if ($scope.LiveInstituteId != 0 && $scope.LiveDepartmentId != 0) {
            $scope.LiveDepartmentId = 0;
            $scope.LiveDepartmentName = "";
            $localStorage.ModifiedDepartmentId = null;
            $localStorage.LiveDepartmentName = null;


        } else if ($scope.LiveInstituteId != 0 && $scope.LiveDepartmentId == 0) {
            $scope.LiveInstituteId = 0;
            $scope.LiveInstituteName = "";
            $localStorage.ModifiedInstituteId = null;
            $localStorage.LiveInstituteName = null;


        } else if ($scope.LiveInstituteId == 0 && $scope.LiveDepartmentId != 0) {
            $scope.LiveDepartmentId = 0;
            $scope.LiveDepartmentName = "";
            $localStorage.ModifiedDepartmentId = null;
            $localStorage.LiveDepartmentName = null;
        }

        callAllMethods();
    }

    /* back button end */

    /* showing institute & department start */
    $scope.ShowInstitute = (($scope.RoleId == 7) && $scope.LiveInstituteId == 0 && $scope.LiveDepartmentId == 0);
    $scope.ShowDepartment = ((($scope.RoleId == 7) && $scope.LiveInstituteId != 0 && $scope.LiveDepartmentId == 0) || (($scope.RoleId == 3 || $scope.RoleId == 8) && $scope.LiveInstituteId == 0 && $scope.LiveDepartmentId == 0));

    $scope.$watch('LiveInstituteId', function (newVal, oldVal) {
        if (newVal !== oldVal) {

            $scope.ShowInstitute = (($scope.RoleId == 7) && $scope.LiveInstituteId == 0 && $scope.LiveDepartmentId == 0);
            $scope.ShowDepartment = ((($scope.RoleId == 7) && $scope.LiveInstituteId != 0 && $scope.LiveDepartmentId == 0));
        }
    });

    $scope.$watch('LiveDepartmentId', function (newVal, oldVal) {
        if (newVal !== oldVal) {

            $scope.ShowDepartment = ((($scope.RoleId == 7) && $scope.LiveInstituteId != 0 && $scope.LiveDepartmentId == 0) || (($scope.RoleId == 3 || $scope.RoleId == 8) && $scope.LiveInstituteId == 0 && $scope.LiveDepartmentId == 0));
        }
    });

    /* showing institute & department end */

    /* ------------------------ Changing CurrentYear and call methods again End --------------------------------------- */


    /* ------------------------ ng-click Function Start --------------------------------------- */

    /* Active tab function start */

    function ActiveTab(tab) {
        $('.nav-tabs a[data-target="#' + tab + '"]').tab('show');
    };

    /* Active tab function End */


    /* StundetStat Functions Start (All Student Related Functions are starting from here) */

    $scope.StudentStat = function () {
        //alert("THis is from StudentStat")
        $scope.LiveEntity = 'studentsPanel';
        $scope.LiveStat = "student_application";
        ActiveTab('studentsPanel');
        ActiveTab('student_application');
        $scope.ApplicationStatistics();
    };

    $scope.StudentApplicationStat = function () {

        $scope.LiveEntity = 'studentsPanel';
        $scope.LiveStat = 'student_application';
        ActiveTab('studentsPanel');
        ActiveTab('student_application');
        $scope.ApplicationStatistics();

    };

    $scope.StudentAdmissionStat = function () {

        $scope.LiveEntity = 'studentsPanel';
        $scope.LiveStat = 'student_admission';
        ActiveTab('studentsPanel');
        ActiveTab('student_admission');
        $scope.AdmissionStatistics();

    };

    $scope.StudentPre_ExaminationStat = function () {

        $scope.LiveEntity = 'studentsPanel';
        $scope.LiveStat = 'student_pre_examination';
        ActiveTab('studentsPanel');
        ActiveTab('student_pre_examination');
        $scope.PreExaminationStatistics();

    };

    $scope.StudentExaminationStat = function () {

        $scope.LiveEntity = 'studentsPanel';
        $scope.LiveStat = 'student_examination';
        ActiveTab('studentsPanel');
        ActiveTab('student_examination');

    };

    $scope.StudentAcademicStat = function () {

        $scope.LiveEntity = 'studentsPanel';
        $scope.LiveStat = 'student_academics';
        ActiveTab('studentsPanel');
        ActiveTab('student_academics');

    };

    /* StundetStat Function End */


    /* TeacherStat Function Start (All teacher related functions are starting from here)*/

    $scope.TeacherStat = function () {
        //alert("This is from TeacherStat");
        $scope.LiveEntity = 'teachersPanel';
        //$scope.LiveStat = 'teacher_';
        ActiveTab('teachersPanel');
        //ActiveTab('student_application');
    };

    /* TeacherStat Function End */


    /* ------------------------ ng-click Function End --------------------------------------- */

    /* ------------------------ View side changing using javascript and JQuery Start --------------------------------------- */


    window.onscroll = function () { scrollFunction() };

    function scrollFunction() {
        if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
            $('#fixed-go-up-icon').css("display", "block");
        } else {
            $('#fixed-go-up-icon').css("display", "none");
        }
    }

    $("#fixed-go-up-icon").click(function () {
        //document.body.scrollTop = 0;
        document.body.scrollTo({ top: 0, behavior: 'smooth' });
        //document.documentElement.scrollTop = 0;
        document.documentElement.scrollTo({ top: 0, behavior: 'smooth' });
    });

    $("#All-Institutes").hover(function () {
        $('#All-Institute-MegaMenu-Outer').css("display", "inline");
    },
        function () {
            $('#All-Institute-MegaMenu-Outer').css("display", "none");
        });


    $("#All-Institute-MegaMenu").hover(function () {
        $('#All-Institute-MegaMenu-Outer').css("display", "inline");
    },
        function () {
            $('#All-Institute-MegaMenu-Outer').css("display", "none");
        });

    $("#SocialCategories").hover(function () {
        $('#SocialCategoriesList').modal('show');
    });

    $("#category").mouseleave(function () {
        $('#SocialCategoriesList').modal('hide');
    });

    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF

    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }




    /* ------------------------ View side changing using javascript and JQuery End --------------------------------------- */


    /* ------------------------ Statistics draw functions And API calls Start --------------------------------------- */

    /* Go To Next State Start */

    $scope.GoToNextState = function (topping, checkname, StatTag) {

        $('#SocialCategoriesList').modal('hide');
        $state.go('dashboarddetailsadmin', {
            Heading: topping,
            checkName: checkname,
            statTag: StatTag,
            facultyName: $scope.LiveInstituteName,
            departmentName: $scope.LiveDepartmentName,
            academicYear: $scope.LiveAcademicYear,
            modifiedInstituteId: $scope.LiveInstituteId,
            modifiedDepartmentId: $scope.LiveDepartmentId,

            modifiedExamEventId: $scope.LiveExamEventId
        });

    }

    /* Go To Next State End */

    /* General Statistics Start */

    $scope.GeneralStatistics = function () {

        //console.log("arrived in General Statistics");

        /* declaring functions */

        // Function for GeneralStatistics API Calling

        $scope.GetGeneralStatistics = function () {
            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/GetStudentStatistics/MstGeneralStat',
                data: {
                    "AcademicYear": $scope.LiveAcademicYear,
                    "ModifiedInstituteId": $scope.LiveInstituteId,
                    "ModifiedSubjectId": $scope.LiveDepartmentId
                },
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    //$scope.offSpinner();

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        $scope.GeneralTableParams = new NgTableParams({}, { dataset: response.obj });
                        $scope.GeneralStatusInfo = response.obj;
                        //console.log("data from response", $scope.GeneralStatusInfo);
                        SettingVariable($scope.GeneralStatusInfo);
                    }

                })
                .error(function (res) {
                    console.log("error in from api");
                    $scope.offSpinner();
                    $rootScope.$broadcast('dialog', "Error", "alert", res);
                });

        }

        let SettingVariable = function (data) {
            $scope.TotalStudents = data[0].TotalStudents;
            $scope.TotalTeachers = data[0].TotalTeachers;
            $scope.TotalInstitutes = data[0].TotalInstitutes;
            $scope.TotalDepartments = data[0].TotalSubjects;
            $scope.TotalPrograms = data[0].TotalPrograms;
            $scope.TotalStudentAchivements = data[0].TotalStudentAchivements;
            $scope.TotalTeacherAchivements = data[0].TotalTeacherAchivements;
            $scope.Faculties = data[0].Facultylist;
            $scope.Colleges = data[0].Collegelist;
            $scope.Departments = data[0].Subjectlist;
        }

        //calling functions

        $scope.GetGeneralStatistics();

    };

    /* General Statistics End */


    /* Application Statistics Start */

    $scope.ApplicationStatistics = function () {

        //console.log("arrived in ApplicationStatistics");

        /* declaring functions */

        // Function For ApplicationStatus API Calling

        let GetApplicationStatistics = function () {
            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/GetStudentStatistics/MstStudentApplicationStat',
                data: {
                    "AcademicYear": $scope.LiveAcademicYear,
                    "ModifiedInstituteId": $scope.LiveInstituteId,
                    "ModifiedSubjectId": $scope.LiveDepartmentId
                },
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $scope.offSpinner();

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        $scope.ApplicationTableParams = new NgTableParams({}, { dataset: response.obj });
                        $scope.ApplicationStatusInfo = response.obj;
                        //$state.go('FacultyEdit');
                        //console.log($scope.ApplicationStatusInfo);
                        DrawApplicationStatisticsMainChart($scope.ApplicationStatusInfo);
                        DrawApplicationStatisticsFeeChart($scope.ApplicationStatusInfo);
                        DrawApplicationStatisticsCancelledOrRejectedChart($scope.ApplicationStatusInfo);
                        DrawApplicationStatisticsPendingStatusChart($scope.ApplicationStatusInfo);
                    }

                })
                .error(function (res) {
                    console.log("error in from api");
                    $scope.offSpinner();
                    $rootScope.$broadcast('dialog', "Error", "alert", res);
                });


        };

        let DrawApplicationStatisticsMainChart = function (chartData) {

            //console.log("arrived in Application Main Bar Chart");
            //console.log(chartData)
            //console.log(chartData[0].AcademicYear)

            google.charts.load('current', { packages: ['corechart', 'bar'] });
            google.charts.setOnLoadCallback(drawStacked);


            function drawStacked() {

                var ApplicationStatusData = google.visualization.arrayToDataTable([
                    ['Application Status', 'Number Of Applications', { role: 'annotation' }],
                    ['Total Application Arrived', chartData[0].TotalApplications, chartData[0].TotalApplications],
                    ['Approved By Academics', chartData[0].ApprovedApplications, chartData[0].ApprovedApplications],
                    ['Pending Approvement By Academics', chartData[0].TotalPendingApplications, chartData[0].TotalPendingApplications],
                    ['Cancelled or Rejected', chartData[0].TotalCancelledorRejectedApplications, chartData[0].TotalCancelledorRejectedApplications]

                ]);


                var options = {
                    title: 'Application statistics',
                    titleTextStyle: {
                        fontSize: 18
                    },
                    chartArea: { width: '70%', height: '80%' },
                    isStacked: true,
                    chartArea: { width: '50%' },
                    annotations: {
                        alwaysOutside: true,
                        textStyle: {
                            fontSize: 18,
                            bold: true,
                            color: '#000000'
                        }
                    },
                    bar: { groupWidth: "60%" },
                    legend: { position: 'none' },
                    animation: {
                        duration: 1000,
                        startup: true
                    },
                    hAxis: {
                        title: 'Number of Applications',
                        titleTextStyle: {
                            bold: true,
                            fontSize: 18
                        },
                        minValue: 0,
                    },
                    vAxis: {
                        title: 'Application status',
                        titleTextStyle: {
                            bold: true,
                            fontSize: 18
                        }
                    },
                    colors: [LiveColor],
                    height: 320
                };


                var ApplicationStatusBarchart = new google.visualization.BarChart(document.getElementById('ApplicationStatisticsMainChart'));

                // EventHandler function initialize
                function ApplicationStatusClickHandler() {
                    //alert("This is from Application Status");
                    var selectedItem = ApplicationStatusBarchart.getSelection()[0];

                    if (selectedItem) {
                        var topping = ApplicationStatusData.getValue(selectedItem.row, 0);

                        switch (topping) {

                            case 'Total Application Arrived':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'TotalApplications',
                                    statTag: 'Application',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'Approved By Academics':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'ApprovedApplications',
                                    statTag: 'Application',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'Pending Approvement By Academics':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'TotalPendingApplications',
                                    statTag: 'Application',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'Cancelled or Rejected':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'TotalCancelledorRejectedApplications',
                                    statTag: 'Application',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            default:
                                break;

                        }

                        //console.log(topping);
                        //console.log(selectedItem);
                    }
                }

                // click event listener added
                google.visualization.events.addListener(ApplicationStatusBarchart, 'select', ApplicationStatusClickHandler);

                // Draw the chart
                ApplicationStatusBarchart.draw(ApplicationStatusData, options);
            }

        };

        let DrawApplicationStatisticsFeeChart = function (chartData) {

            //console.log("arrived in fee pie chart")

            google.charts.load('current', { 'packages': ['corechart'] });
            google.charts.setOnLoadCallback(drawChart);

            function drawChart() {

                var applicationFeesdata = google.visualization.arrayToDataTable([
                    ['Fee Status', 'No of Students'],
                    ['Paid', chartData[0].ApplicationFeesPaid],
                    ['Unpaid', chartData[0].ApplicationFeesNotPaid]
                ]);

                var options = {
                    title: 'Application Fee Status',
                    titleTextStyle: {
                        fontSize: 18
                    },
                    chartArea: { width: '80%', height: '80%' },
                    pieSliceText: 'value',
                    pieSliceTextStyle: { fontSize: 18 },
                    slices: {
                        0: { color: LiveColor },
                        1: { color: RedColor, offset: 0.05 }
                    },
                    height: 350
                }


                var applicationFeesPieChart = new google.visualization.PieChart(document.getElementById('ApplicationStatisticsFeeChart'));


                // EventHandler function initialize
                function applicationFeesClickHandler() {
                    //alert("This is from pie chart");
                    var selectedItem = applicationFeesPieChart.getSelection()[0];

                    if (selectedItem) {
                        var topping = applicationFeesdata.getValue(selectedItem.row, 0);

                        switch (topping) {

                            case 'Paid':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'ApplicationFeesPaid',
                                    statTag: 'Application',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'Unpaid':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'ApplicationFeesNotPaid',
                                    statTag: 'Application',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            default:
                                break;

                        }

                        //console.log(topping);
                        //console.log(selectedItem);
                    }
                }

                // click event listener added
                google.visualization.events.addListener(applicationFeesPieChart, 'select', applicationFeesClickHandler);

                // Draw the chart
                applicationFeesPieChart.draw(applicationFeesdata, options);
            }

        };

        let DrawApplicationStatisticsCancelledOrRejectedChart = function (chartData) {

            //console.log("arrived in Application Cancelled Or Rejected Bar Chart");
            //console.log(chartData)
            //console.log(chartData[0].AcademicYear)

            google.charts.load('current', { packages: ['corechart', 'bar'] });
            google.charts.setOnLoadCallback(drawStacked);


            function drawStacked() {

                var ApplicationCancelledOrRejectedData = google.visualization.arrayToDataTable([
                    ['Cancelled or Rejected Applications', 'Number Of Applications', { role: 'annotation' }],
                    ['Cancelled or Rejected', chartData[0].TotalCancelledorRejectedApplications, chartData[0].TotalCancelledorRejectedApplications],
                    ['Cancelled Applications By Student', chartData[0].CancelledApplicationsByStudent, chartData[0].CancelledApplicationsByStudent],
                    ['Rejected Applications By Faculty', chartData[0].RejectedApplicationsByFaculty, chartData[0].RejectedApplicationsByFaculty],
                    ['Rejected Applications By Academics', chartData[0].RejectedApplicationsByAcademics, chartData[0].RejectedApplicationsByAcademics]

                ]);


                var options = {
                    title: 'Cancelled or Rejected Application statistics',
                    titleTextStyle: {
                        fontSize: 18
                    },
                    chartArea: { width: '70%', height: '80%' },
                    isStacked: true,
                    chartArea: { width: '50%' },
                    annotations: {
                        alwaysOutside: true,
                        textStyle: {
                            fontSize: 18,
                            bold: true,
                            color: '#000000'
                        }
                    },
                    bar: { groupWidth: "60%" },
                    legend: { position: 'none' },
                    animation: {
                        duration: 1000,
                        startup: true
                    },
                    hAxis: {
                        title: 'Number of Applications',
                        titleTextStyle: {
                            bold: true,
                            fontSize: 18
                        },
                        minValue: 0,
                    },
                    vAxis: {
                        title: 'Cancelled or Rejected Applications',
                        titleTextStyle: {
                            bold: true,
                            fontSize: 18
                        }
                    },
                    colors: [LiveColor],
                    height: 320
                };


                var ApplicationCancelledOrRejectedBarchart = new google.visualization.BarChart(document.getElementById('ApplicationStatisticsCancelledOrRejectedChart'));

                // EventHandler function initialize
                function ApplicationCancelledOrRejectedClickHandler() {
                    //alert("This is from cancelled or rejected applications");
                    var selectedItem = ApplicationCancelledOrRejectedBarchart.getSelection()[0];

                    if (selectedItem) {
                        var topping = ApplicationCancelledOrRejectedData.getValue(selectedItem.row, 0);

                        switch (topping) {

                            case 'Cancelled or Rejected':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'TotalCancelledorRejectedApplications',
                                    statTag: 'Application',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'Cancelled Applications By Student':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'CancelledApplicationsByStudent',
                                    statTag: 'Application',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'Rejected Applications By Faculty':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'RejectedApplicationsByFaculty',
                                    statTag: 'Application',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'Rejected Applications By Academics':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'RejectedApplicationsByAcademics',
                                    statTag: 'Application',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            default:
                                break;

                        };

                        //console.log(topping);
                        //console.log(selectedItem);
                    }
                }

                // click event listener added
                google.visualization.events.addListener(ApplicationCancelledOrRejectedBarchart, 'select', ApplicationCancelledOrRejectedClickHandler);

                // Draw the chart
                ApplicationCancelledOrRejectedBarchart.draw(ApplicationCancelledOrRejectedData, options);
            }

        };

        let DrawApplicationStatisticsPendingStatusChart = function (chartData) {

            //console.log("arrived in Application Pending Status Bar Chart");
            //console.log(chartData)
            //console.log(chartData[0].AcademicYear)

            google.charts.load('current', { packages: ['corechart', 'bar'] });
            google.charts.setOnLoadCallback(drawStacked);


            function drawStacked() {

                var ApplicationPendingStatusData = google.visualization.arrayToDataTable([
                    ['Pending Applications', 'Number Of Applications', { role: 'annotation' }],
                    ['Pending Approvement By Academics', chartData[0].TotalPendingApplications, chartData[0].TotalPendingApplications],
                    ['Untouched By Faculty', chartData[0].PendingVerificationByFaculty, chartData[0].PendingVerificationByFaculty],
                    ['Pending Status By Faculty', chartData[0].PendingStatusByFaculty, chartData[0].PendingStatusByFaculty],
                    ['Untouched By Academics', chartData[0].PendingVerificationByAcademics, chartData[0].PendingVerificationByAcademics],
                    ['Pending Status By Academics', chartData[0].PendingStatusByAcademics, chartData[0].PendingStatusByAcademics],

                ]);

                var options = {
                    title: 'Pending Application statistics',
                    titleTextStyle: {
                        fontSize: 18
                    },
                    chartArea: { width: '70%', height: '80%' },
                    isStacked: true,
                    chartArea: { width: '50%' },
                    annotations: {
                        alwaysOutside: true,
                        textStyle: {
                            fontSize: 18,
                            bold: true,
                            color: '#000000'
                        }
                    },
                    bar: { groupWidth: "60%" },
                    legend: { position: 'none' },
                    animation: {
                        duration: 1000,
                        startup: true
                    },
                    hAxis: {
                        title: 'Number of Applications',
                        titleTextStyle: {
                            bold: true,
                            fontSize: 18
                        },
                        minValue: 0,
                    },
                    vAxis: {
                        title: 'Pending Applications',
                        titleTextStyle: {
                            bold: true,
                            fontSize: 18
                        }
                    },
                    colors: [LiveColor],
                    height: 400
                };

                var ApplicationPendingStatusBarchart = new google.visualization.BarChart(document.getElementById('ApplicationStatisticsPendingStatusChart'));

                // EventHandler function initialize
                function ApplicationPendingStatusClickHandler() {
                    //alert("This is from Pending Status applications");
                    var selectedItem = ApplicationPendingStatusBarchart.getSelection()[0];

                    if (selectedItem) {
                        var topping = ApplicationPendingStatusData.getValue(selectedItem.row, 0);

                        switch (topping) {

                            case 'Pending Approvement By Academics':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'TotalPendingApplications',
                                    statTag: 'Application',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'Untouched By Faculty':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'PendingVerificationByFaculty',
                                    statTag: 'Application',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'Pending Status By Faculty':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'PendingStatusByFaculty',
                                    statTag: 'Application',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'Untouched By Academics':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'PendingVerificationByAcademics',
                                    statTag: 'Application',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'Pending Status By Academics':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'PendingStatusByAcademics',
                                    statTag: 'Application',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            default:
                                break;

                        }

                        console.log(topping);
                        console.log(selectedItem);
                    }
                };

                // click event listener added
                google.visualization.events.addListener(ApplicationPendingStatusBarchart, 'select', ApplicationPendingStatusClickHandler);

                // Draw the chart
                ApplicationPendingStatusBarchart.draw(ApplicationPendingStatusData, options);
            }

        };

        //calling functions

        GetApplicationStatistics();

    };

    /* Application Statistics End */


    /* Admission Statistics Start */

    $scope.AdmissionStatistics = function () {

        //console.log("arrived in AdmissionStatistics");

        /* declaring functions */

        //Function for AdmissionStatus API Calling

        let GetAdmissionStatistics = function () {
            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/GetStudentStatistics/MstStudentAdmissionStat',
                data: {
                    "AcademicYear": $scope.LiveAcademicYear,
                    "ModifiedInstituteId": $scope.LiveInstituteId,
                    "ModifiedSubjectId": $scope.LiveDepartmentId
                },
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $scope.offSpinner();

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        $scope.AdmissionTableParams = new NgTableParams({}, { dataset: response.obj });
                        $scope.AdmissionStatusInfo = response.obj;
                        //console.log($scope.AdmissionStatusInfo);
                        DrawAdmissionStatisticsMainChart($scope.AdmissionStatusInfo);
                        DrawAdmissionStatisticsFeeChart($scope.AdmissionStatusInfo);
                        DrawAdmissionStatisticsEligibilityStatusChart($scope.AdmissionStatusInfo);
                        DrawAdmissionStatisticsGenderStatusChart($scope.AdmissionStatusInfo);
                        DrawAdmissionStatisticsCastChart($scope.AdmissionStatusInfo);
                        DrawAdmissionStatisticsBloodGroupChart($scope.AdmissionStatusInfo);
                        SettingVariable($scope.AdmissionStatusInfo);
                    }

                })
                .error(function (res) {
                    console.log("error in from api");
                    $scope.offSpinner();
                    $rootScope.$broadcast('dialog', "Error", "alert", res);
                });

        };

        let DrawAdmissionStatisticsMainChart = function (chartData) {

            //console.log("arrived in Admission Main Bar Chart");
            //console.log(chartData)
            //console.log(chartData[0].TotalPRNGenerated)

            google.charts.load('current', { packages: ['corechart', 'bar'] });
            google.charts.setOnLoadCallback(drawStacked);


            function drawStacked() {

                var AdmissionStatusData = google.visualization.arrayToDataTable([
                    ['Admission Status', 'Number Of Students', { role: 'annotation' }],
                    ['Total Students', chartData[0].TotalAdmittedStudents, chartData[0].TotalAdmittedStudents],
                    ['PRN Generated', chartData[0].TotalPRNGenerated, chartData[0].TotalPRNGenerated],
                    ['PRN Not Generated', chartData[0].TotalPRNNotGenerated, chartData[0].TotalPRNNotGenerated],
                    ['Paper Selected', chartData[0].TotalPaperSelected, chartData[0].TotalPaperSelected],
                    ['Paper Not Selected', chartData[0].TotalPaperNotSelected, chartData[0].TotalPaperNotSelected]

                ]);


                var options = {
                    title: 'Admission statistics',
                    titleTextStyle: {
                        fontSize: 18
                    },
                    chartArea: { width: '70%', height: '80%' },
                    isStacked: true,
                    chartArea: { width: '50%' },
                    annotations: {
                        alwaysOutside: true,
                        textStyle: {
                            fontSize: 18,
                            bold: true,
                            color: '#000000'
                        }
                    },
                    bar: { groupWidth: "60%" },
                    legend: { position: 'none' },
                    animation: {
                        duration: 1000,
                        startup: true
                    },
                    hAxis: {
                        title: 'Number of Students',
                        titleTextStyle: {
                            bold: true,
                            fontSize: 18
                        },
                        minValue: 0,
                    },
                    vAxis: {
                        title: 'Admission status',
                        titleTextStyle: {
                            bold: true,
                            fontSize: 18
                        }
                    },
                    colors: [LiveColor],
                    height: 400
                };


                var AdmissionStatusBarchart = new google.visualization.BarChart(document.getElementById('AdmissionStatisticsMainChart'));

                // EventHandler function initialize
                function AdmissionStatusClickHandler() {

                    //alert('The user selected bar chart');
                    var selectedItem = AdmissionStatusBarchart.getSelection()[0];

                    if (selectedItem) {
                        var topping = AdmissionStatusData.getValue(selectedItem.row, 0);

                        switch (topping) {

                            case 'Total Students':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'TotalAdmittedStudents',
                                    statTag: 'Admission',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'PRN Generated':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'TotalPRNGenerated',
                                    statTag: 'Admission',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'PRN Not Generated':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'TotalPRNNotGenerated',
                                    statTag: 'Admission',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'Paper Selected':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'TotalPaperSelected',
                                    statTag: 'Admission',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'Paper Not Selected':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'TotalPaperNotSelected',
                                    statTag: 'Admission',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            default:
                                break;

                        }
                        console.log(topping);
                        console.log(selectedItem);
                    }
                }

                // click event listener added
                google.visualization.events.addListener(AdmissionStatusBarchart, 'select', AdmissionStatusClickHandler);

                // Draw the chart
                AdmissionStatusBarchart.draw(AdmissionStatusData, options);

            }

        };

        let DrawAdmissionStatisticsFeeChart = function (chartData) {

            //console.log("arrived in Admission Fees Bar Chart");
            //console.log(chartData)
            //console.log(chartData[0].TotalPRNGenerated)

            google.charts.load('current', { packages: ['corechart', 'bar'] });
            google.charts.setOnLoadCallback(drawStacked);


            function drawStacked() {

                var AdmissionFeeData = google.visualization.arrayToDataTable([
                    ['Admission Fees Status', 'Number Of Students', { role: 'annotation' }],
                    ['Total Students', chartData[0].TotalAdmittedStudents, chartData[0].TotalAdmittedStudents],
                    ['Fees Paid', chartData[0].StudentFeesPaid, chartData[0].StudentFeesPaid],
                    ['Fully Fees Paid', chartData[0].StudentPaidFullyFees, chartData[0].StudentPaidFullyFees],
                    ['Partialy Fees Paid', chartData[0].StudentPaidPartialAdmissionFees, chartData[0].StudentPaidPartialAdmissionFees],
                    ['Fees Not Paid', chartData[0].StudentNotPaidAdmissionFees, chartData[0].StudentNotPaidAdmissionFees]
                ]);


                var options = {
                    title: 'Admission Fees statistics',
                    titleTextStyle: {
                        fontSize: 18
                    },
                    chartArea: { width: '70%', height: '80%' },
                    isStacked: true,
                    chartArea: { width: '50%' },
                    annotations: {
                        alwaysOutside: true,
                        textStyle: {
                            fontSize: 18,
                            bold: true,
                            color: '#000000'
                        }
                    },
                    bar: { groupWidth: "60%" },
                    legend: { position: 'none' },
                    animation: {
                        duration: 1000,
                        startup: true
                    },
                    hAxis: {
                        title: 'Number of Students',
                        titleTextStyle: {
                            bold: true,
                            fontSize: 18
                        },
                        minValue: 0,
                    },
                    vAxis: {
                        title: 'Admission Fees status',
                        titleTextStyle: {
                            bold: true,
                            fontSize: 18
                        }
                    },
                    colors: [LiveColor],
                    height: 400
                };


                var AdmissionFeeBarchart = new google.visualization.BarChart(document.getElementById('AdmissionStatisticsFeeChart'));

                // EventHandler function initialize
                function AdmissionFeeClickHandler() {

                    //alert('The user selected bar chart');
                    var selectedItem = AdmissionFeeBarchart.getSelection()[0];

                    if (selectedItem) {
                        var topping = AdmissionFeeData.getValue(selectedItem.row, 0);

                        switch (topping) {

                            case 'Total Students':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'TotalAdmittedStudents',
                                    statTag: 'Admission',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'Fees Paid':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'StudentFeesPaid',
                                    statTag: 'Admission',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'Fully Fees Paid':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'StudentPaidFullyFees',
                                    statTag: 'Admission',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'Partialy Fees Paid':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'StudentPaidPartialAdmissionFees',
                                    statTag: 'Admission',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'Fees Not Paid':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'StudentNotPaidAdmissionFees',
                                    statTag: 'Admission',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            default:
                                break;

                        }

                    }
                }

                // click event listener added
                google.visualization.events.addListener(AdmissionFeeBarchart, 'select', AdmissionFeeClickHandler);

                // Draw the chart
                AdmissionFeeBarchart.draw(AdmissionFeeData, options);

            }

        };

        let DrawAdmissionStatisticsEligibilityStatusChart = function (chartData) {

            //console.log("arrived in  Eligibility Status chart")

            google.charts.load('current', { 'packages': ['corechart'] });
            google.charts.setOnLoadCallback(drawChart);

            function drawChart() {

                var admissionEligibilitydata = google.visualization.arrayToDataTable([
                    ['Eligibility Status', 'No of Students'],
                    ['Eligible', chartData[0].TotalEligible],
                    ['Provisionally Eligible', chartData[0].TotalProvisionallyEligible]
                ]);

                var options = {
                    title: 'Admission Eligibility Status',
                    titleTextStyle: {
                        fontSize: 18
                    },
                    chartArea: { width: '80%', height: '80%' },
                    pieSliceText: 'value',
                    pieSliceTextStyle: { fontSize: 20 },
                    slices: {
                        0: { color: LiveColor },
                        1: { color: lightOrangeColor, offset: 0.05 }
                    },
                    height: 350
                }

                var admissionEligibilityPieChart = new google.visualization.PieChart(document.getElementById('AdmissionStatisticsEligibilityChart'));

                // EventHandler function initialize
                function admissionEligibilityClickHandler() {

                    //alert("This is from Eligibility Status pie chart");
                    var selectedItem = admissionEligibilityPieChart.getSelection()[0];

                    if (selectedItem) {
                        var topping = admissionEligibilitydata.getValue(selectedItem.row, 0);

                        switch (topping) {

                            case 'Eligible':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'TotalEligible',
                                    statTag: 'Admission',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'Provisionally Eligible':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'TotalProvisionallyEligible',
                                    statTag: 'Admission',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            default:
                                break;

                        }

                        console.log(topping);
                        console.log(selectedItem);
                    }
                }

                // click event listener added
                google.visualization.events.addListener(admissionEligibilityPieChart, 'select', admissionEligibilityClickHandler);

                // Draw the chart
                admissionEligibilityPieChart.draw(admissionEligibilitydata, options);
            }

        };

        let DrawAdmissionStatisticsGenderStatusChart = function (chartData) {

            //console.log("arrived in  Gender Status chart")

            google.charts.load('current', { 'packages': ['corechart'] });
            google.charts.setOnLoadCallback(drawChart);

            function drawChart() {

                var admissionGenderdata = google.visualization.arrayToDataTable([
                    ['Gender', 'No of Students'],
                    ['Male', chartData[0].MaleStudents],
                    ['Female', chartData[0].FemaleStudents],
                    ['Other', chartData[0].OtherStudents]
                ]);

                var options = {
                    title: 'Gender Of Students',
                    titleTextStyle: {
                        fontSize: 18
                    },
                    chartArea: { width: '90%', height: '85%' },
                    pieSliceText: 'value',
                    pieSliceTextStyle: { fontSize: 20 },
                    slices: {
                        0: { color: LiveColor },
                        1: { color: RedColor },
                        2: { color: lightOrangeColor }
                    },
                    height: 350
                };


                var admissionGenderPieChart = new google.visualization.PieChart(document.getElementById('AdmissionStatisticsGenderChart'));

                // EventHandler function initialize
                function admissionGenderClickHandler() {
                    //alert("This is from Gender Status pie chart");
                    var selectedItem = admissionGenderPieChart.getSelection()[0];

                    if (selectedItem) {

                        var topping = admissionGenderdata.getValue(selectedItem.row, 0);
                        switch (topping) {

                            case 'Male':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'MaleStudents',
                                    statTag: 'Admission',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'Female':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'FemaleStudents',
                                    statTag: 'Admission',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'Other':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'OtherStudents',
                                    statTag: 'Admission',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            default:
                                break;

                        }

                        console.log(topping);
                        console.log(selectedItem);
                    }
                }

                // click event listener added
                google.visualization.events.addListener(admissionGenderPieChart, 'select', admissionGenderClickHandler);

                // Draw the chart
                admissionGenderPieChart.draw(admissionGenderdata, options);

            }

        };

        let DrawAdmissionStatisticsCastChart = function (chartData) {

            //console.log("arrived in Admission Main Bar Chart");
            //console.log(chartData)
            //console.log(chartData[0].TotalPRNGenerated)

            google.charts.load('current', { packages: ['corechart', 'bar'] });
            google.charts.setOnLoadCallback(drawStacked);


            function drawStacked() {

                var AdmissionCastData = google.visualization.arrayToDataTable([
                    ['Student Cast Status', 'Number Of Students', { role: 'annotation' }],
                    ['Total Students', chartData[0].TotalAdmittedStudents, chartData[0].TotalAdmittedStudents],
                    ['GENERAL', chartData[0].GENERALStudents, chartData[0].GENERALStudents],
                    ['EWS', chartData[0].EWSStudents, chartData[0].EWSStudents],
                    ['SEBC', chartData[0].SEBCStudents, chartData[0].SEBCStudents],
                    ['ST', chartData[0].STStudents, chartData[0].STStudents],
                    ['SC', chartData[0].SCStudents, chartData[0].SCStudents]

                ]);


                var options = {
                    title: 'Student Cast statistics',
                    titleTextStyle: {
                        fontSize: 18
                    },
                    chartArea: { width: '70%', height: '80%' },
                    isStacked: true,
                    chartArea: { width: '50%' },
                    annotations: {
                        alwaysOutside: true,
                        textStyle: {
                            fontSize: 18,
                            bold: true,
                            color: '#000000'
                        }
                    },
                    bar: { groupWidth: "60%" },
                    legend: { position: 'none' },
                    animation: {
                        duration: 1000,
                        startup: true
                    },
                    hAxis: {
                        title: 'Number of Students',
                        titleTextStyle: {
                            bold: true,
                            fontSize: 18
                        },
                        minValue: 0,
                    },
                    vAxis: {
                        title: 'Student Cast status',
                        titleTextStyle: {
                            bold: true,
                            fontSize: 18
                        }
                    },
                    colors: [LiveColor],
                    height: 480
                };


                var AdmissionCastBarchart = new google.visualization.BarChart(document.getElementById('AdmissionStatisticsCastChart'));

                // EventHandler function initialize
                function AdmissionCastClickHandler() {

                    //alert('The user selected bar chart');
                    var selectedItem = AdmissionCastBarchart.getSelection()[0];

                    if (selectedItem) {

                        var topping = AdmissionCastData.getValue(selectedItem.row, 0);

                        switch (topping) {

                            case 'Total Students':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'TotalAdmittedStudents',
                                    statTag: 'Admission',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'GENERAL':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'GENERALStudents',
                                    statTag: 'Admission',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'EWS':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'EWSStudents',
                                    statTag: 'Admission',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'SEBC':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'SEBCStudents',
                                    statTag: 'Admission',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'ST':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'STStudents',
                                    statTag: 'Admission',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'SC':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'SCStudents',
                                    statTag: 'Admission',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            default:
                                break;

                        }

                        console.log(topping);
                        console.log(selectedItem);
                    }
                }

                // click event listener added
                google.visualization.events.addListener(AdmissionCastBarchart, 'select', AdmissionCastClickHandler);

                // Draw the chart
                AdmissionCastBarchart.draw(AdmissionCastData, options);

            }

        };

        let DrawAdmissionStatisticsBloodGroupChart = function (chartData) {

            //console.log("arrived in Admission Main Bar Chart");
            //console.log(chartData)
            //console.log(chartData[0].TotalPRNGenerated)

            google.charts.load('current', { packages: ['corechart', 'bar'] });
            google.charts.setOnLoadCallback(drawStacked);


            function drawStacked() {

                var AdmissionBloodGroupData = google.visualization.arrayToDataTable([
                    ['Blood Group Wise Students', 'Number Of Students', { role: 'annotation' }],
                    ['A+ Blood Group', chartData[0].APosBloodGroupStudents, chartData[0].APosBloodGroupStudents],
                    ['A- Blood Group', chartData[0].ANegBloodGroupStudents, chartData[0].ANegBloodGroupStudents],
                    ['B+ Blood Group', chartData[0].BPosBloodGroupStudents, chartData[0].BPosBloodGroupStudents],
                    ['B- Blood Group', chartData[0].BNegBloodGroupStudents, chartData[0].BNegBloodGroupStudents],
                    ['O+ Blood Group', chartData[0].OPosBloodGroupStudents, chartData[0].OPosBloodGroupStudents],
                    ['O- Blood Group', chartData[0].ONegBloodGroupStudents, chartData[0].ONegBloodGroupStudents],
                    ['AB+ Blood Group', chartData[0].ABPosBloodGroupStudents, chartData[0].ABPosBloodGroupStudents],
                    ['AB- Blood Group', chartData[0].ABNegBloodGroupStudents, chartData[0].ABNegBloodGroupStudents]
                ]);


                var options = {
                    title: 'Blood Group wise Student statistics',
                    titleTextStyle: {
                        fontSize: 18
                    },
                    chartArea: { width: '50%', height: '80%' },
                    isStacked: true,
                    annotations: {
                        alwaysOutside: true,
                        textStyle: {
                            fontSize: 18,
                            bold: true,
                            color: '#000000'
                        }
                    },
                    bar: { groupWidth: "60%" },
                    legend: { position: 'none' },
                    animation: {
                        duration: 1000,
                        startup: true
                    },
                    hAxis: {
                        title: 'Number of Students',
                        titleTextStyle: {
                            bold: true,
                            fontSize: 18
                        },
                        minValue: 0,
                    },
                    vAxis: {
                        title: 'Blood Group wise Student statistics',
                        titleTextStyle: {
                            bold: true,
                            fontSize: 18
                        }
                    },
                    colors: [LiveColor],
                    height: 640
                };


                var AdmissionBloodGroupBarchart = new google.visualization.BarChart(document.getElementById('AdmissionStatisticsBloodGroupChart'));

                // EventHandler function initialize
                function AdmissionBloodGroupClickHandler() {

                    //alert('The user selected bar chart');
                    var selectedItem = AdmissionBloodGroupBarchart.getSelection()[0];

                    if (selectedItem) {

                        var topping = AdmissionBloodGroupData.getValue(selectedItem.row, 0);

                        switch (topping) {

                            case 'A+ Blood Group':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'APosBloodGroupStudents',
                                    statTag: 'Admission',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'A- Blood Group':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'ANegBloodGroupStudents',
                                    statTag: 'Admission',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'B+ Blood Group':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'BPosBloodGroupStudents',
                                    statTag: 'Admission',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'B- Blood Group':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'BNegBloodGroupStudents',
                                    statTag: 'Admission',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'O+ Blood Group':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'OPosBloodGroupStudents',
                                    statTag: 'Admission',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'O- Blood Group':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'ONegBloodGroupStudents',
                                    statTag: 'Admission',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'AB+ Blood Group':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'ABPosBloodGroupStudents',
                                    statTag: 'Admission',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'AB- Blood Group':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'ABNegBloodGroupStudents',
                                    statTag: 'Admission',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            default:
                                break;

                        }

                        console.log(topping);
                        console.log(selectedItem);
                    }
                }

                // click event listener added
                google.visualization.events.addListener(AdmissionBloodGroupBarchart, 'select', AdmissionBloodGroupClickHandler);

                // Draw the chart
                AdmissionBloodGroupBarchart.draw(AdmissionBloodGroupData, options);

            }

        };

        let SettingVariable = function (data) {
            $scope.NRIStudents = data[0].NRIStudents;
            $scope.LocalStudents = data[0].LocalStudents;
            $scope.PhysicallyChallengedStudents = data[0].PhysicallyChallengedStudents;

            $scope.SocialCategory = [
                { "Category": "Ex-serviceman/Ward of Ex-serviceman", "Students": data[0].ExServiceStudents, "Cname": "ExServiceStudents" },
                { "Category": "Active Serviceman/ Ward of Active Serviceman", "Students": data[0].ActServiceStudents, "Cname": "ActServiceStudents" },
                { "Category": "Freedom Fighter/ Ward of Freedom Fighter", "Students": data[0].WFFStudents, "Cname": "WFFStudents" },
                { "Category": "Ward of Primary Teacher", "Students": data[0].WPTStudents, "Cname": "WPTStudents" },
                { "Category": "Ward of Secondary Teacher", "Students": data[0].WSTStudents, "Cname": "WSTStudents" },
                { "Category": "Deserted/Divorced/ Widowed Women", "Students": data[0].DDWStudents, "Cname": "DDWStudents" },
                { "Category": "Member of Project Affected Family", "Students": data[0].MPAFStudents, "Cname": "MPAFStudents" },
                { "Category": "Member of Earthquake Affected Family", "Students": data[0].EAFStudents, "Cname": "EAFStudents" },
                { "Category": "Member of Flood/Famine Affected Family", "Students": data[0].FAFStudents, "Cname": "FAFStudents" },
                { "Category": "Residence of Tribal Area", "Students": data[0].RTAStudents, "Cname": "RTAStudents" },
                { "Category": "Kashmir Migrant", "Students": data[0].KMStudents, "Cname": "KMStudents" },
                { "Category": "Admission Under TFW Quota", "Students": data[0].TFWStudents, "Cname": "TFWStudents" },
                { "Category": "Defense Person", "Students": data[0].DPStudents, "Cname": "DPStudents" },
                { "Category": "MSUB Staff Quota", "Students": data[0].MSUBSQStudents, "Cname": "MSUBSQStudents" },
                { "Category": "Not Applicable", "Students": data[0].NAStudents, "Cname": "NAStudents" },
                { "Category": "Sports Quota", "Students": data[0].SQStudents, "Cname": "SQStudents" }
            ];
        }

        //calling functions

        GetAdmissionStatistics();


    };

    /* Admission Statistics End */

    /* Pre-Examination Statistics Start */

    $scope.PreExaminationStatistics = function () {

        //console.log("arrived in PreExaminationStatistics");

        /* declaring functions */

        // Function For ApplicationStatus API Calling

        let GetPreExaminationStatistics = function () {
            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/GetStudentStatistics/MstStudentPreExaminationStat',
                data: {
                    "AcademicYear": $scope.LiveAcademicYear,
                    "ModifiedInstituteId": $scope.LiveInstituteId,
                    "ModifiedSubjectId": $scope.LiveDepartmentId,
                    "ModifiedExamEventId": $scope.LiveExamEventId

                },
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $scope.offSpinner();

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        $scope.PreExaminationTableParams = new NgTableParams({}, { dataset: response.obj });
                        $scope.PreExaminationStatusInfo = response.obj;
                        //console.log($scope.PreExaminationStatusInfo);
                        DrawPreExaminationStatisticsMainChart($scope.PreExaminationStatusInfo);
                        DrawPreExaminationStatisticsTimeTableConfigureChart($scope.PreExaminationStatusInfo);
                        DrawPreExaminationExamScheduledChart($scope.PreExaminationStatusInfo);
                        SettingVariable($scope.PreExaminationStatusInfo);
                    }

                })
                .error(function (res) {
                    console.log("error in from api");
                    $scope.offSpinner();
                    $rootScope.$broadcast('dialog', "Error", "alert", res);
                });


        };

        let DrawPreExaminationStatisticsMainChart = function (chartData) {

            //console.log("arrived in Application Main Bar Chart");
            //console.log(chartData)
            //console.log(chartData[0].AcademicYear)

            google.charts.load('current', { packages: ['corechart', 'bar'] });
            google.charts.setOnLoadCallback(drawStacked);


            function drawStacked() {

                var PreExaminationStatusData = google.visualization.arrayToDataTable([
                    ['Pre-Examination Status', 'Number Of Students', { role: 'annotation' }],
                    ['Total Active Students', chartData[0].TotalActiveStudents, chartData[0].TotalActiveStudents],
                    ['Students Paper Selected', chartData[0].PaperSelected, chartData[0].PaperSelected],
                    ['Students Not Paper Selected', chartData[0].NotSelectedPaper, chartData[0].NotSelectedPaper],
                    ['Exam Form Generated', chartData[0].ExamFormsGenereated, chartData[0].ExamFormsGenereated],
                    ['Exam Form Not Generated', chartData[0].ExamFormsNotGenereated, chartData[0].ExamFormsNotGenereated]
                ]);


                var options = {
                    title: 'Pre-Examination statistics',
                    titleTextStyle: {
                        fontSize: 18
                    },
                    chartArea: { width: '70%', height: '80%' },
                    isStacked: true,
                    chartArea: { width: '50%' },
                    annotations: {
                        alwaysOutside: true,
                        textStyle: {
                            fontSize: 18,
                            bold: true,
                            color: '#000000'
                        }
                    },
                    bar: { groupWidth: "60%" },
                    legend: { position: 'none' },
                    animation: {
                        duration: 1000,
                        startup: true
                    },
                    hAxis: {
                        title: 'Number of Students',
                        titleTextStyle: {
                            bold: true,
                            fontSize: 18
                        },
                        minValue: 0,
                    },
                    vAxis: {
                        title: 'Pre-Examination status',
                        titleTextStyle: {
                            bold: true,
                            fontSize: 18
                        }
                    },
                    colors: [LiveColor],
                    height: 320
                };


                var PreExaminationStatusBarchart = new google.visualization.BarChart(document.getElementById('PreExaminationStatisticsMainChart'));

                // EventHandler function initialize
                function PreExaminationStatusClickHandler() {
                    //alert("This is from PreExamination Status");
                    var selectedItem = PreExaminationStatusBarchart.getSelection()[0];

                    if (selectedItem) {
                        var topping = PreExaminationStatusData.getValue(selectedItem.row, 0);

                        switch (topping) {

                            case 'Total Active Students':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'TotalActiveStudents',
                                    statTag: 'Pre-Examination',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'Students Paper Selected':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'PaperSelected',
                                    statTag: 'Pre-Examination',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'Students Not Paper Selected':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'NotSelectedPaper',
                                    statTag: 'Pre-Examination',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'Exam Form Generated':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'ExamFormsGenereated',
                                    statTag: 'Pre-Examination',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'Exam Form Not Generated':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'ExamFormsNotGenereated',
                                    statTag: 'Pre-Examination',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            default:
                                break;

                        }

                        console.log(topping);
                        console.log(selectedItem);
                    }
                }

                // click event listener added
                google.visualization.events.addListener(PreExaminationStatusBarchart, 'select', PreExaminationStatusClickHandler);

                // Draw the chart
                PreExaminationStatusBarchart.draw(PreExaminationStatusData, options);
            }

        };

        let DrawPreExaminationStatisticsTimeTableConfigureChart = function (chartData) {

            //console.log("arrived in fee pie chart")

            google.charts.load('current', { 'packages': ['corechart'] });
            google.charts.setOnLoadCallback(drawChart);

            function drawChart() {

                var PreExaminationTimeTableConfiguredata = google.visualization.arrayToDataTable([
                    ['Status of TimeTable Published (Branch wise)', 'No of Students'],
                    ['Published', chartData[0].TimeTableConfigured],
                    ['Unpublished', chartData[0].TimeTableNotConfigured]
                ]);

                var options = {
                    title: 'Status of TimeTable Published (Branch wise)',
                    titleTextStyle: {
                        fontSize: 18
                    },
                    chartArea: { width: '80%', height: '80%' },
                    pieSliceText: 'value',
                    pieSliceTextStyle: { fontSize: 18 },
                    slices: {
                        0: { color: LiveColor },
                        1: { color: RedColor, offset: 0.05 }
                    },
                    height: 350
                }


                var PreExaminationTimeTableConfigurePieChart = new google.visualization.PieChart(document.getElementById('PreExaminationTimeTableConfigureChart'));


                // EventHandler function initialize
                function PreExaminationTimeTableConfigureClickHandler() {
                    //alert("This is from pie chart");
                    var selectedItem = PreExaminationTimeTableConfigurePieChart.getSelection()[0];

                    if (selectedItem) {
                        var topping = PreExaminationTimeTableConfiguredata.getValue(selectedItem.row, 0);

                        switch (topping) {

                            case 'Published':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'TimeTableConfigured',
                                    statTag: 'Pre-Examination',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'Unpublished':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'TimeTableNotConfigured',
                                    statTag: 'Pre-Examination',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            default:
                                break;

                        }

                        console.log(topping);
                        console.log(selectedItem);
                    }
                }

                // click event listener added
                google.visualization.events.addListener(PreExaminationTimeTableConfigurePieChart, 'select', PreExaminationTimeTableConfigureClickHandler);

                // Draw the chart
                PreExaminationTimeTableConfigurePieChart.draw(PreExaminationTimeTableConfiguredata, options);
            }

        };

        let DrawPreExaminationExamScheduledChart = function (chartData) {

            //console.log("arrived in fee pie chart")

            google.charts.load('current', { 'packages': ['corechart'] });
            google.charts.setOnLoadCallback(drawChart);

            function drawChart() {

                var PreExaminationExamScheduleddata = google.visualization.arrayToDataTable([
                    ['Status of Exam Scheduled (Faculty wise)', 'No of Institutes'],
                    ['Scheduled', chartData[0].TotalExamScheduled],
                    ['Not Scheduled', chartData[0].TotalExamNotScheduled]
                ]);

                var options = {
                    title: 'Status of Exam Scheduled (Faculty wise)',
                    titleTextStyle: {
                        fontSize: 18
                    },
                    chartArea: { width: '80%', height: '80%' },
                    pieSliceText: 'value',
                    pieSliceTextStyle: { fontSize: 18 },
                    slices: {
                        0: { color: LiveColor },
                        1: { color: RedColor, offset: 0.05 }
                    },
                    height: 350
                }


                var PreExaminationExamScheduledPieChart = new google.visualization.PieChart(document.getElementById('PreExaminationExamScheduledChart'));


                // EventHandler function initialize
                function PreExaminationExamScheduledClickHandler() {
                    //alert("This is from pie chart");
                    var selectedItem = PreExaminationExamScheduledPieChart.getSelection()[0];

                    if (selectedItem) {
                        var topping = PreExaminationExamScheduleddata.getValue(selectedItem.row, 0);

                        switch (topping) {

                            case 'Scheduled':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'TotalExamScheduled',
                                    statTag: 'Pre-Examination',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,
                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            case 'Not Scheduled':
                                $state.go('dashboarddetailsadmin', {
                                    Heading: topping,
                                    checkName: 'TotalExamNotScheduled',
                                    statTag: 'Pre-Examination',
                                    facultyName: $scope.LiveInstituteName,
                                    departmentName: $scope.LiveDepartmentName,
                                    academicYear: $scope.LiveAcademicYear,
                                    modifiedInstituteId: $scope.LiveInstituteId,
                                    modifiedDepartmentId: $scope.LiveDepartmentId,

                                    modifiedExamEventId: $scope.LiveExamEventId
                                });
                                break;
                            default:
                                break;

                        }

                        console.log(topping);
                        console.log(selectedItem);
                    }
                }

                // click event listener added
                google.visualization.events.addListener(PreExaminationExamScheduledPieChart, 'select', PreExaminationExamScheduledClickHandler);

                // Draw the chart
                PreExaminationExamScheduledPieChart.draw(PreExaminationExamScheduleddata, options);
            }

        };

        let SettingVariable = function (data) {
            $scope.TotalRepeaters = data[0].TotalRepeaters;
            $scope.TotalProgrammePartTermExams = data[0].TotalProgrammePartTermExams;
            $scope.ExamEventList = data[0].ExamEventDetails;
            $scope.CurrentExamEventId = data[0].CurrentExamEventId;
            $scope.CurrentExamEventName = data[0].CurrentExamEventName;
        };

        //calling functions

        GetPreExaminationStatistics();

    };

    $scope.ChangeExamEventId = function () {

        $scope.PreExaminationStatistics();

    };

    /* Pre-Examination Statistics End */


    /* ------------------------ Statistics draw functions And API calls End --------------------------------------- */
});