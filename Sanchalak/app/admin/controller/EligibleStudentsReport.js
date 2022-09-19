app.controller('EligibleStudentsReportCtrl', function ($scope, $http, $filter, $localStorage, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }
    $scope.FlagList2 = false;
    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }


    $scope.EligibleStudentsReport = {}

    $scope.TakeLocalStoradeValue = function () {
        //debugger
        if ($localStorage.Stats.FlagFromAppStats == true) {
            $localStorage.Stats.FlagFromAppStats = false;
            $scope.EligibleStudentsReport.ProgrammeInstancePartTermId = $localStorage.Stats.ProgramInstancePartTermId;
            $scope.getEligibleStudentsReport();
        }
        else {
            $localStorage.Stats = null;
            $scope.EligibleStudentsReport = null;
        }
    };


    $scope.GetLocalStorageValue = function () {
        //debugger
        if ($localStorage.Stats.FlagFromAppStats == true) {
            $localStorage.Stats.FlagFromAppStats = false;
            $scope.EligibleStudentsReport.ProgrammeInstancePartTermId = $localStorage.Stats.ProgramInstancePartTermId;
            $localStorage.FacultyId = $localStorage.Stats.FacultyId;
            $localStorage.AcademicYearId = $localStorage.Stats.AcademicYearId;
            $localStorage.InstituteId = $localStorage.Stats.InstituteId;
            $scope.getEligibleStudentsReport();
        }
        else {
            $localStorage.Stats = null;
            $scope.EligibleStudentsReport = null;
        }
    };

    // This method is for getting InstancePartTerm By Institute
    $scope.getIncProgInsPartTermListByInstituteId = function () {


        //debugger
        $http({
            method: 'Post',
            url: 'api/EligibleStudentsReport/IncProgrammeInstancePartTermGetByFIdAIdGet',
            data: {
                FacultyId: $localStorage.Stats.FacultyId, AcademicYearId: $localStorage.Stats.AcademicYearId,
                InstituteId: $localStorage.Stats.InstituteId
            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $scope.alGet = response.obj;
                $localStorage.Stats = {};
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.GetPartTermList = function () {
        //debugger
        if ($localStorage.Stats.FacultyId == null || $localStorage.Stats.FacultyId == undefined ||
            $localStorage.Stats.AcademicYearId == null || $localStorage.Stats.AcademicYearId == undefined ||
            $localStorage.Stats.InstituteId == null || $localStorage.Stats.InstituteId) { $scope.getIncProgInsPartTermListByInstituteId(); }

    };


    // This method is for getting InstancePartTerm
    $scope.ApplicationListGet = function () {
        //alert("Institute");

        $http({
            method: 'GET',
            url: 'api/EligibleStudentsReport/ApplicationListGet',
            data: {
                FacultyId: $localStorage.Stats.FacultyId, AcademicYearId: $localStorage.Stats.AcademicYearId,
                InstituteId: $localStorage.Stats.InstituteId
            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $scope.alGet = response.obj;
                $localStorage.Stats = {};
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getInstituteById = function () {
        //debugger;
        $http({
            method: 'POST',
            url: 'api/EligibleStudentsReport/InstituteGetById',
            data: { Id: $localStorage.InstituteId },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                // debugger;
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                $scope.Institute = response.obj[0];

                // $scope.Faculty = response.obj; // Krunal's code               



            })
            .error(function (res) {
                alert(res);
            });
    };



    //Get Students Admission Fees Paid Report
    $scope.getEligibleStudentsReport = function (ProgrammeInstancePartTermId) {
        $scope.FlagList2 = true;
        //alert($scope.AdmittedStudentByAcademicsFaculty.ProgrammeInstancePartTermId);
        //var InsPartTerm = $('#InsPartTerm').val();
        if ($scope.EligibleStudentsReport.ProgrammeInstancePartTermId == "" ||
            $scope.EligibleStudentsReport.ProgrammeInstancePartTermId == null ||
            $scope.EligibleStudentsReport.ProgrammeInstancePartTermId == undefined) {

            alert("Please select Programme Instance Part Term");
            return false;
        }
        $http({
            method: 'POST',
            url: 'api/EligibleStudentsReport/EligibleStudentsReportGet',
            data: {
                ProgrammeInstancePartTermId: $scope.EligibleStudentsReport.ProgrammeInstancePartTermId,
                InstituteId: $localStorage.InstituteId
            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                alert("Please wait, Data is processing...");
                //$scope.onSpinner();

                if (response.response_code != "200") {
                    //debugger;
                    if (response.obj == "The source contains no DataRows.") {

                        alert("No Record Found!");
                        $scope.EligibleStudentsReportTableParams = new NgTableParams({
                        }, {
                            dataset: null
                        });

                        $scope.exportDataFull = undefined;
                        //$scope.searchCaseResultFull = undefined;
                        //$scope.offSpinner();
                    }
                    else {

                        alert(response.obj);
                        //$scope.offSpinner();
                    }
                }
                else {
                    $scope.EligibleStudentsReportTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                    $scope.ES = response.obj[0];
                    $scope.ExcelEligible = response.obj;
                    //$scope.ApplicationListSearchFull();
                }
                /*console.log("=====");
                console.log($scope.ApplicationListTableParams);*/
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.exportEligible = function () {

        //if ($scope.ApplicationStatisticsData == undefined) {

        //    alert("Please select Programme Instance Part Term then click on Search");
        //    return false;
        //}
        //debugger
        var FacultyName = $scope.ES.FacultyName;
        var AcademicYearCode = $scope.ES.AcademicYearCode;
        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "EligibleStudentsReport_" + DateWithoutDashed + time;

        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title: 'FacultyName: ' + FacultyName + '<br>' +
                    '   Academic Year: ' + AcademicYearCode + '<br>' +
                    '   Eligible Students Report | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'AcademicYearCode', title: 'Academic Year' },
                { columnid: 'PRN', title: 'PRN' },
                { columnid: 'ApplicationId', title: ' Application No.' },
                { columnid: 'FullName', title: 'Name' },
                { columnid: 'InstituteName', title: 'Institute Name' },
                { columnid: 'EligibilityByAcademics', title: 'Academics Status' },
                { columnid: 'FacultyRemarks', title: 'Faculty Remarks' },
                { columnid: 'AcademicsRemarks', title: 'Academics Remarks' },
                { columnid: 'PRNGeneratedOn', title: 'PRN Generated Date' },


            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.ExcelEligible]);

        // var FacultyName = $scope.Institute.FacultyName;
        // var InstituteName = $scope.Institute.InstituteName;
        // var LongDate = new Date($.now());
        // //alert(LongDate);

        // var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        // //alert(ShortDate);

        // var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        // //alert(DateWithoutDashed);

        // var time = "_" + LongDate.getHours() + ":" + LongDate.getMinutes() + ":" + LongDate.getSeconds();                
        //// alert(time);

        // var dateAndTime = ShortDate + time;
        // var ExcelFileName = "ApplicationStatistics_" + ShortDate + time;     
        // //var ExcelFileName = "ApplicationStatistics_" + DateWithoutDashed + time;

        // var uri = 'data:application/vnd.ms-excel;base64,'           
        //     , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table><h2>Faculty Name: {FacultyName}, Institute Name: {InstituteName}</h2><h2>Application Statistics ({dateAndTime}) </h2>{table}</table></body></html>'
        //     , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
        //     , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }



        // var table = document.getElementById("ApplicationStatsId");                
        // var filters = $('.ng-table-filters').remove();
        // var ctx = { worksheet: name || 'Worksheet', dateAndTime: dateAndTime || dateAndTime, FacultyName: FacultyName || FacultyName, InstituteName: InstituteName || InstituteName, table: table.innerHTML};       

        // $('.ng-table-sort-header').after(filters);
        // var url = uri + base64(format(template, ctx));

        // var a = document.createElement('a');
        // a.href = url;
        // a.download = ExcelFileName + '.xls';
        // a.click();
    };



    $scope.backToList = function () {
        // debugger;

        //alert($localStorage.FacultyId);
        //alert($localStorage.AcademicYearId);
        if ($localStorage.FacultyId == null || $localStorage.FacultyId == undefined ||
            $localStorage.AcademicYearId == null || $localStorage.AcademicYearId == undefined ||
            $localStorage.InstituteId == null || $localStorage.InstituteId == undefined) {
            $state.go('EligibiltyStatisticsReport');
        }
        else {


            $rootScope.Checkls = true;
            $state.go('EligibiltyStatisticsReport');
        }
    };



});



