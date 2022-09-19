app.controller('RefundCaseCancelRefundReportCtrl', function ($scope, $http, $rootScope, Upload, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {
    $rootScope.pageTitle = "Refund Process Cancel by Account";
   
    $scope.RefundCaseCancelAccount = {};
    $scope.RefundAmountObj = {};
    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }

    $scope.getFacultyById = function () {
     
        $http({
            method: 'POST',
            url: 'api/RefundCaseCancelRefundReport/MstFacultyGet',
            data: $scope.RefundCaseCancelAccount,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.FacultyList = response.obj;
             
                //$scope.PostProgInst.FacultyId = $scope.Faculty.Id;
                //$scope.getIncProgPartTermByFacIdList();
            })
            .error(function (res) {
                alert(res);
            });
    };

    //Function for check that BOB or Other
    $scope.getBankStatus = function () {
        
        if ($scope.RefundCaseCancelAccount.Id == null || $scope.RefundCaseCancelAccount.Id === undefined) {
            alert("Please select Faculty...!");
        }
        else if ($scope.RefundCaseCancelAccount.RefundBankStatus == null || $scope.RefundCaseCancelAccount.RefundBankStatus == "" || $scope.RefundCaseCancelAccount.RefundBankStatus === undefined) {
            alert("Please select Bank Status...!");
        }
        else if ($scope.RefundCaseCancelAccount.RefundBankStatus == 'BOB') {
            $scope.BOBFlag = true;
            $scope.RefCaseCancelAccountApplicantsListBOB();
        }
        else if ($scope.RefundCaseCancelAccount.RefundBankStatus == 'Other') {
            $scope.BOBFlag = false;
            $scope.RefCaseCancelAccountApplicantsListOther();
        }

    };

    //Function for get Applicant list for Refund Process in BOB Account
    $scope.RefCaseCancelAccountApplicantsListBOB = function () {

        $scope.RefundCaseBOBTableparam = new NgTableParams({

        },
            { dataset: [] });

        $scope.checkDataforBOB = false;
        //var FacultyId = { FacultyId: $scope.RefundCaseCancelAccount.Id };
        $scope.RefundCaseCancelAccount.FacultyId = $scope.RefundCaseCancelAccount.Id;

        if ($scope.RefundCaseCancelAccount.Id == null || $scope.RefundCaseCancelAccount.Id === undefined) {
            alert("Please select Faculty...!");
        }
        else {
            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/RefundCaseCancelRefundReport/RefCaseCancelAccountApplicantsListBOB',
                data: $scope.RefundCaseCancelAccount,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    // $scope.ApplicantByProgPartTermList = response.obj;
                    $scope.RefundCaseBOBTableparam = new NgTableParams({

                    },
                        { dataset: response.obj });
                    $scope.offSpinner();
                    $scope.ExportFullDataforRefund = response.obj;
                    if (response.obj.length == 0 || response.obj == 'No Records Found.') {
                        $scope.checkDataforBOB = true;
                    }
                    $scope.checkDataforOther = true;

                })
                .error(function (res) {
                    alert(res);
                });
        }
    };

    //Function for get Applicant list for Refund Process in Other than BOB Account
    $scope.RefCaseCancelAccountApplicantsListOther = function () {

        $scope.RefundCaseOtherTableparam = new NgTableParams({

        },
            { dataset: [] });
        $scope.checkDataforOther = false;
        //var FacultyId = { FacultyId: $scope.RefundCaseCancelAccount.Id };
        $scope.RefundCaseCancelAccount.FacultyId = $scope.RefundCaseCancelAccount.Id;

        if ($scope.RefundCaseCancelAccount.Id == null || $scope.RefundCaseCancelAccount.Id === undefined) {
            alert("Please select Faculty...!");
        }
        else {
            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/RefundCaseCancelRefundReport/RefCaseCancelAccountApplicantsListOther',
                data: $scope.RefundCaseCancelAccount,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    // $scope.ApplicantByProgPartTermList = response.obj;
                    $scope.RefundCaseOtherTableparam = new NgTableParams({

                    },
                        { dataset: response.obj });
                    $scope.offSpinner();
                    $scope.ExportFullDataforRefund = response.obj;
                    if (response.obj.length == 0 || response.obj == 'No Records Found.') {
                        $scope.checkDataforOther = true;
                    }
                    $scope.checkDataforBOB = true;

                })
                .error(function (res) {
                    alert(res);
                });
        }
    };

    //Excel Students Admission Fees Paid Report
    $scope.ExportRefundDatatoExcelBOB = function () {

        for (var i in $scope.ExportFullDataforRefund) {

            if ($scope.ExportFullDataforRefund[i].ProcessedOnAccount == "" || $scope.ExportFullDataforRefund[i].ProcessedOnAccount == null) {
                $scope.ExportFullDataforRefund[i].ProcessedOnAccount = 'NA';
            }
            if ($scope.ExportFullDataforRefund[i].RequestStatus == "" || $scope.ExportFullDataforRefund[i].RequestStatus == null) {
                $scope.ExportFullDataforRefund[i].RequestStatus = '--';
            }
        }
        alert("Please wait, Excel is being prepared...");

        //var acYear = $('#acYear option:selected').text();
        //$scope.AppFeeReport.AcademicYearCode = acYear;

        //var AcademicYearCode = $scope.AppFeeReport.AcademicYearCode;

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "Refunded_Report_Of_Cancelled_Admission_Case" + DateWithoutDashed + time;

        var mystyleSecond = {
            headers: true,
            style: 'font-size:25px;font-weight:bold;',
            caption: {
                title: 'Refunded Report Of Cancelled Admission Case for Account(BOB to BOB) on ' + DateAndTime,
            },
            columns: [

                { columnid: 'IndexId', title: 'Sr No.' },
                { columnid: 'ProcessedOnAccount', title: 'Account Approved On' },
                { columnid: 'ApplicationId', title: 'ApplicationId' },
                { columnid: 'FullName', title: 'Applicant Name' },
                { columnid: 'FacultyName', title: 'Faculty Name' },
                { columnid: 'InstancePartTermName', title: 'Programme Name' },
                { columnid: 'RequestStatus', title: 'Refund Status' },
                { columnid: 'RefundAmountByAudit', title: 'AMOUNT' },
                { columnid: 'AccountRemark', title: 'AccountRemark' },
                { columnid: 'RefundedTansactionId', title: 'RefundedTansactionId' },
                { columnid: 'IsBOBAccount', title: 'Is BOB Account' },
                { columnid: 'AccountNumber', title: 'BENEFICIARY ACCOUNT NO' },
                { columnid: 'AccountName', title: 'NAME OF THE BENEFICIARY' },
                { columnid: 'BankName', title: 'NAME & BRANCH OF THE BANK' },
                { columnid: 'IFSCCode', title: '11 DIGIT IFSC CODE' },
                { columnid: 'PassbookDoc', title: 'View Passbook Doc' },
                { columnid: 'RTGSForm', title: 'View RTGS Doc' },
                //{ columnid: 'RefundAmountByAcademic', title: 'Refund Amount By Academic' },
                //{ columnid: 'RefundAmountByAudit', title: 'Refund Amount By Audit' },

            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyleSecond, $scope.ExportFullDataforRefund]);
    };

    //Excel Students Admission Fees Paid Report
    $scope.ExportRefundDatatoExcelOther = function () {

        for (var i in $scope.ExportFullDataforRefund) {

            if ($scope.ExportFullDataforRefund[i].ProcessedOnAccount == "" || $scope.ExportFullDataforRefund[i].ProcessedOnAccount == null) {
                $scope.ExportFullDataforRefund[i].ProcessedOnAccount = 'NA';
            }
            if ($scope.ExportFullDataforRefund[i].PassbookDoc == "" || $scope.ExportFullDataforRefund[i].PassbookDoc == null) {
                $scope.ExportFullDataforRefund[i].PassbookDoc = 'NA';
            }
            if ($scope.ExportFullDataforRefund[i].RTGSForm == "" || $scope.ExportFullDataforRefund[i].RTGSForm == null) {
                $scope.ExportFullDataforRefund[i].RTGSForm = 'NA';
            }
            if ($scope.ExportFullDataforRefund[i].RequestStatus == "" || $scope.ExportFullDataforRefund[i].RequestStatus == null) {
                $scope.ExportFullDataforRefund[i].RequestStatus = '--';
            }
        }

        alert("Please wait, Excel is being prepared...");

        //var acYear = $('#acYear option:selected').text();
        //$scope.AppFeeReport.AcademicYearCode = acYear;

        //var AcademicYearCode = $scope.AppFeeReport.AcademicYearCode;

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "Refunded_Report_Of_Cancelled_Admission_Case" + DateWithoutDashed + time;

        var mystyleSecond = {
            headers: true,
            style: 'font-size:25px;font-weight:bold;',
            caption: {
                title: 'Refunded Report Of Cancelled Admission Case for Account(BOB to Other) on ' + DateAndTime,
            },
            columns: [

                { columnid: 'IndexId', title: 'Sr No.' },
                { columnid: 'ProcessedOnAccount', title: 'Account Approved On' },
                { columnid: 'ApplicationId', title: 'ApplicationId' },
                { columnid: 'FullName', title: 'Applicant Name' },
                { columnid: 'FacultyName', title: 'Faculty Name' },
                { columnid: 'InstancePartTermName', title: 'Programme Name' },
                { columnid: 'RequestStatus', title: 'Refund Status' },
                { columnid: 'RefundAmountByAudit', title: 'AMOUNT' },
                { columnid: 'AccountRemark', title: 'AccountRemark' },
                { columnid: 'RefundedTansactionId', title: 'RefundedTansactionId' },
                { columnid: 'IsBOBAccount', title: 'Is BOB Account' },
                { columnid: 'AccountNumber', title: 'BENEFICIARY ACCOUNT NO' },
                { columnid: 'AccountName', title: 'NAME OF THE BENEFICIARY' },
                { columnid: 'BankName', title: 'NAME & BRANCH OF THE BANK' },
                { columnid: 'IFSCCode', title: '11 DIGIT IFSC CODE' },
                { columnid: 'PassbookDoc', title: 'View Passbook Doc' },
                { columnid: 'RTGSForm', title: 'View RTGS Doc' },
                //{ columnid: 'RefundAmountByAcademic', title: 'Refund Amount By Academic' },
                //{ columnid: 'RefundAmountByAudit', title: 'Refund Amount By Audit' },

            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyleSecond, $scope.ExportFullDataforRefund]);
    };

    $scope.cancelRefundCaseList = function () {
        $scope.RefundCaseCancelAccount = {};
        $scope.showFormFlag = false;
    };

});



