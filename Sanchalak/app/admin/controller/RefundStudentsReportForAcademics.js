app.controller('RefundStudentsReportForAcademicsCtrl', function ($scope, $http, $rootScope, $window, $state, $filter, $cookies, $mdDialog, $localStorage, NgTableParams) {


    $rootScope.pageTitle = "RefundStudentsReportForAcademics";

    var InstPartList = [];
    

    //$scope.AppCount = 0;
   

    $scope.RefundStudentsReportForAcademics = {};

   
    

/*Acadmics Refund Student Report*/

    $scope.getRefundStudentsReportForAcademics = function (ProgrammeInstancePartTermId) {

        $http({
            method: 'POST',
            url: 'api/RefundStudentsReportForAcademics/RefundStudentsReportForAcademicsGet',
            data: { ProgrammeInstancePartTermId: $scope.RefundStudentsReportForAcademics.ProgrammeInstancePartTermId },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                // $scope.ApplicantByProgPartTermList = response.obj;

                if (response.obj == "No Record Found") {
                    alert("No Record Found");
                    $scope.RefundStudentsReportForAcademicsTableParams = new NgTableParams({

                    }, {
                        dataset: []
                    });

                }
                else {

                    $scope.RefundStudentsReportForAcademicsTableParams = new NgTableParams({

                    }, {


                        dataset: response.obj

                    });
                };
                $scope.exportDataFull = response.obj;

            })
            .error(function (res) {
                alert(res);
            });




    };

    $scope.getFacultyList = function () {

        $http({
            method: 'POST',
            url: 'api/RefundStudentsReportForAcademics/FacultyGet',
            data: $scope.RefundStudentsReportForAcademics,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.FacList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getIncProgrammeInstancePartTerm = function () {

        $http({
            method: 'POST',
            url: 'api/RefundStudentsReportForAcademics/ProgPartTermGetByFacultyId',
            data: { FacultyId: $scope.RefundStudentsReportForAcademics.FacultyId },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgInstPartTermList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };


    $scope.exportData = function () {
        //debugger;
        if ($scope.exportDataFull == undefined) {

            alert("Please select Programme Instance Part Term then click on Search");
            return false;
        }
        //alert("Please wait, Excel is being prepared...Do not click again on Export to Excel button until Excel is download");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "BlockApplicantsFromAdmission_" + DateWithoutDashed + time;

        var mystyleSecond = {
            headers: true,
            style: 'font-size:25px;font-weight:bold;',
            caption: {
                title: 'Block Applicants From Admission | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'Id', title: 'Application Id' },
                { columnid: 'AdmissionBlocked', title: 'Admission Blocked' },
                { columnid: 'FullName', title: 'Full Name' },
                { columnid: 'ApplicantUserName', title: 'Applicant User Name.' },
                { columnid: 'EmailId', title: 'Email Id' },
                { columnid: 'MobileNo', title: 'Mobile No' },
                { columnid: 'FeeCategoryName', title: 'Fee Category Name' },
                { columnid: 'IsVerificationEmailOns', title: 'Is Verification Email On' },
                { columnid: 'IsVerificationSmsOns', title: 'Is Verification Sms On' },


            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyleSecond, $scope.exportDataFull]);
    };

});