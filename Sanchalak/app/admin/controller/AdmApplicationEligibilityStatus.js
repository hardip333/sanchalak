app.controller('AdmApplicationEligibilityStatusCtrl', function ($scope, $http, $filter, $localStorage, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage AdmApplicationEligibilityStatus";

    $scope.AdmApplicationEligibilityStatus = {}

    

    $scope.test = "Pending";

    $scope.InstPartList = []

    /*Reset Academic Year Level*/
    $scope.resetAdmApplicationEligibilityStatus = function () {
        $scope.AdmApplicationEligibilityStatus = {};
    };

    /*Get Application Payment Report*/
    $scope.getAdmApplicationEligibilityStatus = function () {
        //debugger;
        //var data = new Object();
        
        var data = {

            FacultyId: $localStorage.facultyDepartIntituteId,
            AcademicYearId: $scope.AdmApplicationEligibilityStatus.AcademicYearId,
            ProgrammeInstancePartTermId: $scope.AdmApplicationEligibilityStatus.ProgInstPartTermId,
            EmailId: $scope.AdmApplicationEligibilityStatus.EmailId
            //ApplicantRegistrationId: $scope.AdmApplicationEligibilityStatus.ApplicantRegistrationId,
            //MobileNo: $scope.AdmApplicationEligibilityStatus.MobileNo,
            //FacultyEmail: $scope.AdmApplicationEligibilityStatus.FacultyEmail,
            //ProgrammeName: $scope.AdmApplicationEligibilityStatus.ProgrammeName,
            //BranchName: $scope.AdmApplicationEligibilityStatus.BranchName,
            //EligibilityStatus: $scope.AdmApplicationEligibilityStatus.EligibilityStatus,
            //AdminRemarkByFaculty: $scope.AdmApplicationEligibilityStatus.AdminRemarkByFaculty
        }
        //if ($scope.AdmApplicationEligibilityStatus.AcademicYearId == null || $scope.AdmApplicationEligibilityStatus.AcademicYearId == undefined ||
        //    $scope.AdmApplicationEligibilityStatus.ProgInstPartTermId == null || $scope.AdmApplicationEligibilityStatus.ProgInstPartTermId == undefined) {

        //    alert("Please Select Above DropDown")
        //}
        //else {
            $http({
                method: 'POST',
                url: 'api/AdmApplicationEligibilityStatus/AdmApplicationEligibilityStatusGet',
                data: data,
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

                    //$scope.getApplicationPaymentReport = response.obj;
                    //$scope.refreshtable();
                    $scope.AdmApplicationEligibilityStatusTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj


                    });
                    
                    
                }

            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
        //}

        
    };
    /*Get Application Payment Report*/


    /*Get Academic Year*/
    $scope.getAcademicYear = function () {

        $http({
            method: 'POST',
            url: 'api/AdmApplicationEligibilityStatus/AcademicYearGet',
            data: $scope.AdmApplicationEligibilityStatus,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.AYList = response.obj;
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };    
/*End Academic Year*/

    /*Get Academic Year*/
    $scope.getProgInstPartTerm = function () {
        $http({
            method: 'POST',
            url: 'api/AdmApplicationEligibilityStatus/IncProgrammeInstancePartTermGet',
            data: $scope.AdmApplicationEligibilityStatus,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgInstPartTermList = response.obj;
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };  
    /*End Academic Year*/





    $scope.expand_row = function (Id) {
        let element = document.getElementById('expand' + Id).classList
        if (element.contains("collapse")) {
            document.getElementById("first_col" + Id).innerHTML = "-"
            element.remove("collapse")
        } else {
            document.getElementById("first_col" + Id).innerHTML = "+"
            element.add("collapse")
        }
    }

    $scope.test1 = "Pending";

    $scope.getAdmApplicationEligibilityStatusdata = function () {
        $scope.test1 = $scope.test;
    }

    $scope.exportDataFull = function () {
        //debugger;
        //alert($localStorage.facultyName);
        //alert($scope.MstTransferRequestByFaculty.AcademicYearCode);
        var FacultyName = $localStorage.facultyName;
        //var AcademicCode = $scope.MstTransferRequestByFaculty.AcademicYearCode;

        var LongDate = new Date($.now());
        //alert(LongDate);

        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        //alert(ShortDate);

        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        //alert(DateWithoutDashed);

        var time = "_" + LongDate.getHours() + ":" + LongDate.getMinutes() + ":" + LongDate.getSeconds();
        // alert(time);

        var dateAndTime = ShortDate + time;
        var ExcelFileName = "AdmApplicationEligibilityStatus_" + ShortDate + time;
        //var ExcelFileName = "ApplicationStatistics_" + DateWithoutDashed + time;

        var uri = 'data:application/vnd.ms-excel;base64,'
            , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table><h2>Faculty Name: {FacultyName}</h2><h2>Destination Request ({dateAndTime}) </h2>{table}</table></body></html>'
            , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
            , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }

        var table = document.getElementById("applicationeligiblestatus");
        //var filters = $('.ng-table-filters').remove();
        var ctx = { worksheet: name || 'Worksheet', dateAndTime: dateAndTime || dateAndTime, FacultyName: FacultyName || facultyName, table: table.innerHTML };

        //$('.ng-table-sort-header').after(filters);
        var url = uri + base64(format(template, ctx));

        var a = document.createElement('a');
        a.href = url;
        a.download = ExcelFileName + '.xls';
        a.click();
    };

    $scope.AdmApplicationEligibilityStatusAction = function (RegId,FName,MName,LName,ProgName,BName,EStatus,FEmail,MNo,EId) {
        $http({
            method: 'POST',
            url: 'api/AdmApplicationEligibilityStatus/AdmApplicationEligibilityStatusAction',
            data: { ApplicantRegistrationId: RegId, FirstName: FName, MiddleName: MName, LastName: LName, ProgrammeName: ProgName, BranchName: BName, EligibilityStatus: EStatus, FacultyEmail: FEmail, MobileNo: MNo, EmailId: EId },
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
                        $scope.AdmApplicationEligibilityStatus = {};
                        $scope.getAdmApplicationEligibilityStatus();

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });

        
    }
    

});

        