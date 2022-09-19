app.controller('ApplicationStatisticsCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Application Statistics";

    $scope.cardTitle = "Application Statistics Operation";


    //$scope.ApplicationStatisticsTableParams = new NgTableParams({
    //}, {
    //        dataset: $scope.AppStats
    //});

    $scope.resetApplicationStatistics = function () {
        $scope.AppStats = {};
    };
    $scope.resetProgStatistics = function () {
        $scope.ProgStats = {};
    };

    $scope.expand_row = function (Id) {
        let element = document.getElementById('expand' + Id).classList
        if (element.contains("collapse")) {
            document.getElementById("first_col" + Id).innerHTML = "-"
            element.remove("collapse")
        } else {
            document.getElementById("first_col" + Id).innerHTML = "+"
            element.add("collapse")
        }
    };

    $scope.getMstInstitute = function () {
        
        $http({
            method: 'POST',
            url: 'api/ApplicationStatistics/MstInstituteGet',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.InstituteList = response.obj;
                //$scope.getMstInstituteInstNameById();
                //debugger
                if ($rootScope.Checkls == true) {

                    $scope.AppStats = {};
                    $scope.AppStats.InstituteId = $localStorage.InstId;
                    //$scope.getApplicationStatisticsListByInstituteId();
                    //$scope.getApplicationStatisticsListByFacultyId();
                }
                else {                   

                    $localStorage.InstId = {};
                }
                
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getMstInstituteInstNameById = function () {
        if ($scope.ProgStats.InstituteId > 0) {
            var Institute = { InstituteId: $scope.ProgStats.InstituteId };
            $http({
                method: 'POST',
                url: 'api/ApplicationStatistics/MstInstituteGetInstNameById',
                data: Institute,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $scope.InstituteNameList = response.obj[0];
                    $scope.ProgStats.InstituteName = $scope.InstituteNameList.InstituteName;

                })
                .error(function (res) {
                    alert(res);
                });
        }
        if ($scope.ProgStats.InstituteId == 0)
        {
            $scope.ProgStats.InstituteName = "All Faculty";
        }
    };

    $scope.getIncAcadYearCodeById = function () {
        var AcadId = { AcademicYearId: $scope.ProgStats.AcademicYearId };
        $http({
            method: 'POST',
            url: 'api/ApplicationStatistics/IncAcadYearCodeGetById',
            data: AcadId,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.AcadYearCodeList = response.obj[0];
                $scope.ProgStats.AcademicYearCode = $scope.AcadYearCodeList.AcademicYearCode;
               

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getFacultyById = function () {

        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGetbyId',
          
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                $scope.Institute = response.obj[0];
                // $scope.Faculty = response.obj; // Krunal's code               
               
                //$scope.getApplicationStatisticsListByFacultyId();
              
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getAcademicList = function () {

        $http({
            method: 'POST',
            url: 'api/MstProgramInstance/AcademicYearGet',
           // data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.AcadList = response.obj;
               
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getApplicationStatisticsListByFacultyId = function () {
      
        var obj = {};
        var obj = { InstituteId: $scope.Institute.InstituteId, AcademicYearId: $scope.Institute.AcademicYearId };
  
        $http({
            method: 'POST',
            url: 'api/ApplicationStatistics/ApplicationStatisticsGetByFacultyId',
            data: obj,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $go.state('login');
                }
                else
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                    
                        $scope.ApplicationStatisticsTableParams = new NgTableParams({},
                            { dataset: response.obj });
                        $scope.ApplicationStatisticsData = response.obj;
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getApplicationStatisticsListByInstituteId = function () { 
    
        var obj = {};
        var obj = { InstituteId: $scope.AppStats.InstituteId, AcademicYearId: $scope.AppStats.AcademicYearId };
       
        $http({
            method: 'POST',
            url: 'api/ApplicationStatistics/ApplicationStatisticsGetByFacultyId',
            data: obj,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $go.state('login');
                }
                else
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
    }
                    else {

                        $scope.ApplicationStatisticsByInstIdTableParams = new NgTableParams({},
                            { dataset: response.obj });
                        $scope.ApplicationStatisticsByInstIdData = response.obj;
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
   
    $scope.getProgrammeStatsListByInstIdAndAcdId = function () {
      
        var InstId = document.getElementById("Instdropdown1");
        var AcadId = document.getElementById("Acaddropdown1");
        if ((InstId.value == "" || InstId.value == null || InstId.value === undefined) &&
            (AcadId.value == "" || AcadId.value == null || AcadId.value === undefined)
        ) {        
            $scope.modifyUserFlag = true;
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#EligibilityGroupform')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please Select Institute and Academic Year before Submit...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
       else if ($scope.ProgStats.InstituteId == null || $scope.ProgStats.InstituteId === undefined
        ) {
           
            $scope.modifyUserFlag = true;
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#EligibilityGroupform')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please Select Institute before Submit...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else if ($scope.ProgStats.AcademicYearId == null || $scope.ProgStats.AcademicYearId === undefined
        ) {
            $scope.modifyUserFlag = true;
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#EligibilityGroupform')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please Select Academic Year before Submit...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
            var ProgId = {
                InstituteId: $scope.ProgStats.InstituteId,
                AcademicYearId: $scope.ProgStats.AcademicYearId
            };

            $http({
                method: 'POST',
                url: 'api/ApplicationStatistics/ProgeConfigStatsByInstIdAndAcdYear',
                data: ProgId,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;
                    if (response.response_code == "0") {
                        $go.state('login');
                    }
                    else
                        if (response.response_code != "200") {
                            $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        }
                        else {
                            // $scope.ApplicationStatsList = response.obj;

                            $scope.ProgramStatisticsTableParams = new NgTableParams({}, { dataset: response.obj });
                            $scope.ProgramStatisticsForAcadData = response.obj;
                        }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    }

    $scope.getProgrammeStatisticsListByInstIdAndAcdId = function () {
        var AcadId = document.getElementById("AcademicYear1");
        if (AcadId.value == "" || AcadId.value == null || AcadId.value === undefined
        ) {
            $scope.modifyUserFlag = true;
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#EligibilityGroupform')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please Select Academic Year before Submit...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
        var ProgId = {
            InstituteId: $scope.Institute.InstituteId,
            AcademicYearId: $scope.ProgStats.AcademicYearId
        };
       
        $http({
            method: 'POST',
            url: 'api/ApplicationStatistics/ProgeConfigStatsByInstIdAndAcdYear',
            data: ProgId,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $go.state('login');
                }
                else
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                       
                       $scope.ProgramStatisticsTableParams = new NgTableParams({}, { dataset: response.obj });
                        $scope.ProgramStatisticsByFacultyData = response.obj;
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    }
    }

    $scope.move = function (PIPTId) {
        $localStorage.Stats = {};
        $localStorage.Stats.ProgramInstancePartTermId = PIPTId;
        $localStorage.Stats.FlagFromAppStats = true;
        
    };

    $scope.moveAcademic = function (PIPTId) {
        
        $localStorage.Stats = {};
        $localStorage.Stats.ProgramInstancePartTermId = PIPTId;
        $localStorage.Stats.InstituteId = $scope.AppStats.InstituteId;
        $localStorage.Stats.FlagFromAppStats = true;
        
    };

    $scope.exportApplicationStatsData = function () {
       
        //if ($scope.ApplicationStatisticsData == undefined) {

        //    alert("Please select Programme Instance Part Term then click on Search");
        //    return false;
        //}
        var FacultyName = $scope.Institute.FacultyName;
        var InstituteName = $scope.Institute.InstituteName;
        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "ApplicationStatistics_" + DateWithoutDashed + time;

        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title: 'FacultyName: ' + FacultyName +'<br>'+
                    '   Institute Name: ' + InstituteName + '<br>' +
                '  Application Statistics | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'InstancePartTermName', title: 'Programme Instance Part Term' },
                { columnid: 'Intake', title: 'Intake Capacity' },
                { columnid: 'ApplicationFeeNotPaid', title: 'Total Application (Application Fee Not Paid)' },
                { columnid: 'TotalApplication', title: 'Total Application (Application Fee Paid)' },
                { columnid: 'ApprovedByFaculty', title: 'Approved By Faculty' },
                { columnid: 'NotApprovedByFaculty', title: 'Not Approved By Faculty' },
                { columnid: 'NotApprovedByAcademic', title: 'Not Approved By Academic' },
                { columnid: 'AdmissionFeePaid', title: 'Admission Fee Paid' },
                { columnid: 'AdmissionFeeNotPaid', title: 'Admission Fee Not Paid' },
                { columnid: 'AdmittedStudent', title: 'Admitted Student' },
                { columnid: 'PrnGenerated', title: 'PRN Generated' },
                { columnid: 'PaperSelected', title: 'Paper Selected' },
                { columnid: 'CancelledByAcademics', title: 'Cancelled By Academics' },
                { columnid: 'CancelledByFaculty', title: 'Cancelled By Faculty' },
                { columnid: 'CancelledByStudent', title: 'Cancelled By Student' },
   
            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.ApplicationStatisticsData]);

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

    $scope.exportApplicationStatsByInstituteData = function () {

        //if ($scope.ApplicationStatisticsByInstIdData == undefined) {

        //    alert("Please select Programme Instance Part Term then click on Search");
        //    return false;
        //}
        //var FacultyName = $scope.Institute.FacultyName;
        //var InstituteName = $scope.Institute.InstituteName;
        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "ApplicationStatistics_" + DateWithoutDashed + time;

        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title:// 'FacultyName: ' + FacultyName + '<br>' +
                    //'   Institute Name: ' + InstituteName + '<br>' +
                    '  Application Statistics | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'InstancePartTermName', title: 'Programme Instance Part Term' },
                { columnid: 'FacultyName', title: 'Faculty Name' },
                { columnid: 'Intake', title: 'Intake Capacity' },
                { columnid: 'ApplicationFeeNotPaid', title: 'Total Application (Application Fee Not Paid)' },
                { columnid: 'TotalApplication', title: 'Total Application (Application Fee Paid)' },
                { columnid: 'PreVerificationVerifried', title: 'PreVerification By Faculty Verifried' },
                { columnid: 'PreVerificationPending', title: 'PreVerification By Faculty Pending' },
                { columnid: 'PreVerificationNotApproved', title: 'PreVerification By Faculty NotApproved' },
                { columnid: 'PreVerificationPendingVerified', title: 'PreVerification By Faculty PendingVerified' },
                { columnid: 'ApprovedByFaculty', title: 'Approved By Faculty' },
                { columnid: 'NotApprovedByFaculty', title: 'Not Approved By Faculty' },
                { columnid: 'NotApprovedByAcademic', title: 'Not Approved By Academic' },
                { columnid: 'AdmissionFeePaid', title: 'Admission Fee Paid' },
                { columnid: 'AdmissionFeeNotPaid', title: 'Admission Fee Not Paid' },
                { columnid: 'AdmittedStudent', title: 'Admitted Student' },
                { columnid: 'PrnGenerated', title: 'PRN Generated' },
                { columnid: 'PaperSelected', title: 'Paper Selected' },
                { columnid: 'CancelledByAcademics', title: 'Cancelled By Academics' },
                { columnid: 'CancelledByFaculty', title: 'Cancelled By Faculty' },
                { columnid: 'CancelledByStudent', title: 'Cancelled By Student' },

            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.ApplicationStatisticsByInstIdData]);
    };

    $scope.exportProgConfigData = function () {

        if ($scope.ProgramStatisticsByFacultyData == undefined) {

            alert("Please select Academic Year then click on Submit");
            return false;
        }
        var FacultyName = $scope.Institute.FacultyName;
        var InstituteName = $scope.Institute.InstituteName;
        var AcademicYearCode = $scope.ProgStats.AcademicYearCode;
        var InstituteNameWithoutSpase = InstituteName.replace(/\s+/g, '_');
        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = InstituteNameWithoutSpase + "_" + AcademicYearCode + "_ApplicationConfigurationStatistics_" + DateWithoutDashed + time;

        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title: 'FacultyName: ' + FacultyName + '<br>' +
                    '   Institute Name: ' + InstituteName + ' |  Academic Year:  ' + AcademicYearCode + '<br>' +
                    '  Application Configuration Statistics | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                //{ columnid: 'ProgrammeName', title: 'Programme Name' },
                //{ columnid: 'BranchName', title: 'Branch Name' },
                { columnid: 'InstancePartTermName', title: 'Semester/Part Term Name' },
                { columnid: 'EligibilityGroup', title: 'Eligibility Group' },
                { columnid: 'EligibilityGroupComponent', title: 'Eligibility Group Component' },
                { columnid: 'ProgrammeAddOn', title: 'Programme AddOn' },
                { columnid: 'RequiredDocument', title: 'Required Document' },
                { columnid: 'ApplicationConfiguration', title: 'Application Configuration' },
                { columnid: 'ApplicationFeeConfig', title: 'Application Fee Configured' },
                { columnid: 'AppFeePublish', title: 'Application Fee Published' },
                { columnid: 'AdmFeeConfig', title: 'Admission Fee Configured' },
                { columnid: 'AdmFeePublish', title: 'Admission Fee Published' },
            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.ProgramStatisticsByFacultyData]);


        //var FacultyName = $scope.Institute.FacultyName;
        //var InstituteName = $scope.Institute.InstituteName;
        //var AcademicYearCode = $scope.ProgStats.AcademicYearCode;
        //var InstituteNameWithoutSpase = InstituteName.replace(/\s+/g, '_');
        //var LongDate = new Date($.now());
        ////alert(LongDate);

        //var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        ////alert(ShortDate);

        //var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        ////alert(DateWithoutDashed);

        //var time = "_" + LongDate.getHours() + ":" + LongDate.getMinutes() + ":" + LongDate.getSeconds();
        //// alert(time);

        //var dateAndTime = ShortDate + time;
        //var ExcelFileName = InstituteNameWithoutSpase +"_"+ AcademicYearCode+ "_ApplicationConfigurationStatistics_" + ShortDate + time;
        ////var ExcelFileName = "ApplicationStatistics_" + DateWithoutDashed + time;

        //var uri = 'data:application/vnd.ms-excel;base64,'
        //    , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table><h2>Faculty Name: {FacultyName}, Institute Name: {InstituteName}, Academic Year: {AcademicYearCode}</h2><h2>Application Statistics ({dateAndTime}) </h2>{table}</table></body></html>'
        //    , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
        //    , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }



        //var table = document.getElementById("ProgrammeStatsId");
        //var filters = $('.ng-table-filters').remove();
        //var ctx = { worksheet: name || 'Worksheet', dateAndTime: dateAndTime || dateAndTime, FacultyName: FacultyName || FacultyName, InstituteName: InstituteName || InstituteName, AcademicYearCode: AcademicYearCode || AcademicYearCode, table: table.innerHTML };

        //$('.ng-table-sort-header').after(filters);
        //var url = uri + base64(format(template, ctx));

        //var a = document.createElement('a');
        //a.href = url;
        //a.download = ExcelFileName + '.xls';
        //a.click();
    };
   
    $scope.exportProgConfigByInstituteData = function () {
        if ($scope.ProgramStatisticsForAcadData == undefined) {
     
            alert("Please Select Institute and Select Academic Year then click on Submit");
            return false;
        }
        var InstituteName = $scope.ProgStats.InstituteName;
        var AcademicYearCode = $scope.ProgStats.AcademicYearCode;
        var InstituteNameWithoutSpase = InstituteName.replace(/\s+/g, '_');
        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = InstituteNameWithoutSpase + "_" + AcademicYearCode + "_ApplicationConfigurationStatistics_" + DateWithoutDashed + time;

        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title: ' Institute Name: ' + InstituteName + ' |  Academic Year:  ' + AcademicYearCode + '<br>' +
                    '  Application Configuration Statistics | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                //{ columnid: 'ProgrammeName', title: 'Programme Name' },
                //{ columnid: 'BranchName', title: 'Branch Name' },
                { columnid: 'InstancePartTermName', title: 'Semester/Part Term Name' },
                { columnid: 'EligibilityGroup', title: 'Eligibility Group' },
                { columnid: 'EligibilityGroupComponent', title: 'Eligibility Group Component' },
                { columnid: 'ProgrammeAddOn', title: 'Programme AddOn' },
                { columnid: 'RequiredDocument', title: 'Required Document' },
                { columnid: 'ApplicationConfiguration', title: 'Application Configuration' },
                { columnid: 'ApplicationFeeConfig', title: 'Application Fee Configured' },
                { columnid: 'AppFeePublish', title: 'Application Fee Published' },
                { columnid: 'AdmFeeConfig', title: 'Admission Fee Configured' },
                { columnid: 'AdmFeePublish', title: 'Admission Fee Published' },
            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.ProgramStatisticsForAcadData]);




        //var InstituteName = $scope.ProgStats.InstituteName;
        //var AcademicYearCode = $scope.ProgStats.AcademicYearCode;
        //var InstituteNameWithoutSpase = InstituteName.replace(/\s+/g, '_');
        
        //var LongDate = new Date($.now());
        ////alert(LongDate);

        //var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        ////alert(ShortDate);

        //var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        ////alert(DateWithoutDashed);

        //var time = "_" + LongDate.getHours() + ":" + LongDate.getMinutes() + ":" + LongDate.getSeconds();
        //// alert(time);

        //var dateAndTime = ShortDate + time;
        //var ExcelFileName = InstituteNameWithoutSpase +"_"+ AcademicYearCode + "_ApplicationConfigurationStatistics_" + ShortDate + time;
        ////var ExcelFileName = "ApplicationStatistics_" + DateWithoutDashed + time;

        //var uri = 'data:application/vnd.ms-excel;base64,'
        //    , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table><h2>Institute Name-{InstituteName}, Academic Year-{AcademicYearCode}</h2><h2>Application Configuration Statistics ({dateAndTime}) </h2>{table}</table></body></html>'
        //    , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
        //    , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }



        //var table = document.getElementById("ProgrammeStatsByInstituteId");
        //var filters = $('.ng-table-filters').remove();
        //var ctx = { worksheet: name || 'Worksheet', dateAndTime: dateAndTime || dateAndTime, InstituteName: InstituteName || InstituteName, AcademicYearCode: AcademicYearCode || AcademicYearCode,  table: table.innerHTML };

        //$('.ng-table-sort-header').after(filters);
        //var url = uri + base64(format(template, ctx));

        //var a = document.createElement('a');
        //a.href = url;
        //a.download = ExcelFileName + '.xls';
        //a.click();
    };

});