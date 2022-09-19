app.controller('ProvisionallyEligibleStudentsReportCtrl', function ($scope, $http, $filter, $localStorage, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }

    $scope.selectallFlag = true;
    $scope.deselectallFlag = false;

    $scope.selectall = function () {
        for (var i in $scope.ProvisionallyEligibleStudentsReportTableParams.data) {
            $scope.ProvisionallyEligibleStudentsReportTableParams.data[i].ApplicationIdCheck = true;
            
        }
        $scope.selectallFlag = false;
        $scope.deselectallFlag = true;
    }

    $scope.deselectall = function () {
        for (var i in $scope.ProvisionallyEligibleStudentsReportTableParams.data) {
            $scope.ProvisionallyEligibleStudentsReportTableParams.data[i].ApplicationIdCheck = false;

        }
        $scope.selectallFlag = true ;
        $scope.deselectallFlag = false;
    }

    
    

    $scope.ProvisionallyEligibleStudentsReport = {};

    $scope.ProvEligStud = {};

    $scope.TakeLocalStoradeValue = function () {
        //debugger
        if ($localStorage.Stats.FlagFromAppStats == true) {
            $localStorage.Stats.FlagFromAppStats = false;
            $scope.ProvisionallyEligibleStudentsReport.ProgrammeInstancePartTermId = $localStorage.Stats.ProgramInstancePartTermId;
            $scope.getProvisionallyEligibleStudentsReport();
        }
        else {
            $localStorage.Stats = null;
            $scope.ProvisionallyEligibleStudentsReport = null;
        }
    };


    $scope.GetLocalStorageValue = function () {
        //debugger
        if ($localStorage.Stats.FlagFromAppStats == true) {
            $localStorage.Stats.FlagFromAppStats = false;
            $scope.ProvisionallyEligibleStudentsReport.ProgrammeInstancePartTermId = $localStorage.Stats.ProgramInstancePartTermId;
            $localStorage.FacultyId = $localStorage.Stats.FacultyId;
            $localStorage.AcademicYearId = $localStorage.Stats.AcademicYearId;
            $localStorage.InstituteId = $localStorage.Stats.InstituteId;
            //$localStorage.InstituteName = $localStorage.Stats.InstituteName;
            $scope.getProvisionallyEligibleStudentsReport();
        }
        else {
            $localStorage.Stats = null;
            $scope.ProvisionallyEligibleStudentsReport = null;
        }
    };

    $scope.getInstituteById = function () {
        //debugger;
        $http({
            method: 'POST',
            url: 'api/ProvisionallyEligibleStudentsReport/InstituteGetById',
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


    // This method is for getting InstancePartTerm By Institute
    $scope.getIncProgInsPartTermListByInstituteId = function () {
        //debugger
        $http({
            method: 'Post',
            url: 'api/ProvisionallyEligibleStudentsReport/IncProgrammeInstancePartTermGetByFIdAIdGet',
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
            url: 'api/ProvisionallyEligibleStudentsReport/ApplicationListGet',
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


    //Get Students Admission Fees Paid Report
    $scope.getProvisionallyEligibleStudentsReport = function (ProgrammeInstancePartTermId) {
        //debugger
        //alert($scope.AdmittedStudentByAcademicsFaculty.ProgrammeInstancePartTermId);
        //var InsPartTerm = $('#InsPartTerm').val();
        if ($scope.ProvisionallyEligibleStudentsReport.ProgrammeInstancePartTermId == "" ||
            $scope.ProvisionallyEligibleStudentsReport.ProgrammeInstancePartTermId == null ||
            $scope.ProvisionallyEligibleStudentsReport.ProgrammeInstancePartTermId == undefined) {

            alert("Please select Programme Instance Part Term");
            return false;
        }
        $http({
            method: 'POST',
            url: 'api/ProvisionallyEligibleStudentsReport/ProvisionallyEligibleStudentsReportGet',
            data: {
                ProgrammeInstancePartTermId: $scope.ProvisionallyEligibleStudentsReport.ProgrammeInstancePartTermId,
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
                        $scope.ProvisionallyEligibleStudentsReportTableParams = new NgTableParams({
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
                    $scope.ProvisionallyEligibleStudentsReportTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                    $scope.ProvEligStud = response.obj;
                    $scope.PES = response.obj[0];
                    $scope.ExcelProvEligible = response.obj;

                    //$scope.ApplicationId = $scope.ProvEligStud[0].ApplicationId;
                    //$scope.ApplicationListSearchFull();
                }
                /*console.log("=====");
                console.log($scope.ApplicationListTableParams);*/
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.backToList = function () {
        //debugger;
        //alert($localStorage.InstId);
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

    $scope.updateProvisionallyEligibleStudentsReport = function () {
        //debugger
        
        $scope.ProvisionEligibleStudent = "";
        for (var i = 0; i < $scope.ProvEligStud.length; i++) {
            if ($scope.ProvEligStud[i].ApplicationIdCheck) {
                var ApplicationId = $scope.ProvEligStud[i].ApplicationId;
                $scope.ProvisionEligibleStudent += + ApplicationId + ",";
                }
        }
        //debugger
        $scope.ProvisionEligibleStudent = $scope.ProvisionEligibleStudent.slice(0, -1);
        //alert($scope.ProvisionEligibleStudent);
        //console.log($scope.ProvisionallyEligibleStudentsReportTableParams.data);
        //for (var i in $scope.ProvisionallyEligibleStudentsReportTableParams.data) {
        //    $scope.ProvisionallyEligibleStudentsReportTableParams.data[i].EligibilityByAcademics = "Eligible";
        //    $scope.ProvisionallyEligibleStudentsReportTableParams.data[i].AcademicsRemarks = $scope.ProvisionallyEligibleStudentsReport.AcademicsRemarks;
        //}
        $scope.FinalModel = {};
        $scope.FinalModel.ProvisionEligibleStudent = $scope.ProvisionEligibleStudent;
        $scope.FinalModel.AcademicsRemarks = $scope.ProvisionallyEligibleStudentsReport.AcademicsRemarks;
        //alert($scope.FinalModel.ProvisionEligibleStudent);
        //alert($scope.FinalModel.AcademicsRemarks);
            $http({
                method: 'POST',
                url: 'api/ProvisionallyEligibleStudentsReport/ProvisionallyEligibleStudentsReportEdit',
                data: $scope.FinalModel,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        //debugger
                        alert(response.obj);
                        $scope.getProvisionallyEligibleStudentsReport();
                        //$scope.InstPartList = $scope.InstPartList1;
                        $scope.ProvisionallyEligibleStudentsReport.AcademicsRemarks = '';
                        //$scope.flagdisable = true;

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });

        


    }

    $scope.exportProvEligible = function () {

        //if ($scope.ApplicationStatisticsData == undefined) {

        //    alert("Please select Programme Instance Part Term then click on Search");
        //    return false;
        //}
        //debugger
        var FacultyName = $scope.PES.FacultyName;
        var AcademicYearCode = $scope.PES.AcademicYearCode;
        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "ProvisionallyEligibleStudentsReport_" + DateWithoutDashed + time;

        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title: 'FacultyName: ' + FacultyName + '<br>' +
                    '   Academic Year: ' + AcademicYearCode + '<br>' +
                    '  Provisionally Eligible Students Report | Date and Time: ' + DateAndTime,
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
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.ExcelProvEligible]);

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
        //debugger;

        //alert($localStorage.FacultyId);
        //alert($localStorage.AcademicYearId);
        if ($localStorage.FacultyId == null || $localStorage.FacultyId == undefined || $localStorage.AcademicYearId == null || $localStorage.AcademicYearId == undefined) {
            $state.go('EligibiltyStatisticsReport');
        }
        else {


            $rootScope.Checkls = true;
            $state.go('EligibiltyStatisticsReport');
        }
    };
    


});



