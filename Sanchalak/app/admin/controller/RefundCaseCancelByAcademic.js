app.controller('RefundCaseCancelByAcademicCtrl', function ($scope, $http, $rootScope, $localStorage, $filter, $state, $cookies, $mdDialog, NgTableParams) {
    $rootScope.pageTitle = "Refund Process by Academic for Cancel Admission";
   
    $scope.RefundCaseCancel = {};
    $scope.RefundCancelAmountObj = {};

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
            url: 'api/MstFaculty/MstFacultyGet',
            data: $scope.RefundCaseCancel,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.FacultyList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

    //Function for get Applicant list for Refund Process by Faculty for Cancellation Case
    $scope.RefCaseCancelAcademicApplicantsList = function () {
        $scope.checkDataExists = false;
        //var FacultyId = { FacultyId: $scope.RefundCaseCancel.Id };
        $scope.RefundCaseCancel.FacultyId = $scope.RefundCaseCancel.Id;

        if ($scope.RefundCaseCancel.Id == null || $scope.RefundCaseCancel.Id == "" || $scope.RefundCaseCancel.Id === undefined) {
            alert("Please select Faculty...!");
        }
        else {
            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/RefundCaseCancelByAcademic/RefCaseCancelAcademicApplicantsList',
                data: $scope.RefundCaseCancel,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    // $scope.ApplicantByProgPartTermList = response.obj;
                    $scope.RefundCaseCancelTableparam = new NgTableParams({

                    },
                        { dataset: response.obj });
                    $scope.ApplicantListCancelforAcademic = response.obj;
                    $scope.offSpinner();
                    if (response.obj.length == 0) {
                        $scope.checkDataExists = true;
                    }

                })
                .error(function (res) {
                    alert(res);
                });
        }
    };

    $scope.StepToRefundProcess = function (AppId) {
        $localStorage.RefundCaseCancelAppId = AppId;
        $state.go('RefundCaseCancelApplicantDetails');
    };

    $scope.cancelRefundCaseList = function () {
        $scope.RefundCaseCancel = {};
        $scope.showFormFlag = false;
    };

    $scope.setRefundModeValue = function () {

        if ($scope.RefundApplicantList.RefundMode === undefined || $scope.RefundApplicantList.RefundMode == null) {
            $scope.RefundApplicantList.RefundMode = 'Full_Refund';
        }
    };

    $scope.RefundCalculate = function () {

        $scope.PercentageAmount = (($scope.RefundApplicantList.TotalAmount) * 5) / 100;

        if ($scope.RefundApplicantList.RefundMode == 'Partial_Refund') {
            if (($scope.PercentageAmount) > 1000) {
                $scope.RefundCancelAmountObj.CalculateAmount = ($scope.RefundApplicantList.AmounttoRefund) - 1000;
                $scope.SavedCalculateAmount = $scope.RefundCancelAmountObj.CalculateAmount;
            }
            else {
                $scope.RefundCancelAmountObj.CalculateAmount = ($scope.RefundApplicantList.AmounttoRefund) - ($scope.PercentageAmount);
                $scope.SavedCalculateAmount = $scope.RefundCancelAmountObj.CalculateAmount;
            }
            $scope.RefundAmountText = false;
        }
        else {
            $scope.RefundCancelAmountObj.CalculateAmount = $scope.RefundApplicantList.AmounttoRefund;
            $scope.RefundAmountText = true;
        }
      
    };

    //Function for get Applicant list Details for Refund Process by Faculty for Cancellation Case
    $scope.RefCaseCancelAcademicApplicantsDetails = function () {

        $http({
            method: 'POST',
            url: 'api/RefundCaseCancelByAcademic/RefCaseCancelAcademicApplicantsDetails',
            data: { ApplicationId: $localStorage.RefundCaseCancelAppId },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $scope.RefundApplicantList = response.obj[0];
                $scope.RefundApplicantList.AmounttoRefund = $scope.RefundApplicantList.AmountPaid;
                $scope.setRefundModeValue();
                $scope.RefundCalculate();
            })
            .error(function (res) {
                alert(res);
            });

    };

    //Function for update Refund Amount for Cancellation Case
    $scope.RefCaseCancelUpdateAcademicRefAmount = function () {
  
        $scope.RefundCancelAmountObj.ApplicationId = $scope.RefundApplicantList.ApplicationId;
        $scope.RefundCancelAmountObj.RefundAmountByAcademic = $scope.RefundCancelAmountObj.CalculateAmount;
        $scope.RefundCancelAmountObj.AmountPaid = $scope.RefundApplicantList.AmountPaid;
        $scope.RefundCancelAmountObj.RefundMode = $scope.RefundApplicantList.RefundMode;
        $scope.RefundCancelAmountObj.AutoDeductAmount = $scope.SavedCalculateAmount;

        if ($scope.RefundCancelAmountObj.RefundAmountByAcademic == null || $scope.RefundCancelAmountObj.RefundAmountByAcademic == "" || $scope.RefundCancelAmountObj.RefundAmountByAcademic == undefined ) {
            alert("Please Enter Refunded Amount..!");
        }
        else if (($scope.RefundApplicantList.RefundMode == 'Full_Refund') && ($scope.RefundCancelAmountObj.RefundAmountByAcademic != $scope.RefundCancelAmountObj.AmountPaid)) {
            alert("Refunded amount should be same as applicant paid amount. Please check...!");
        }
        //else if (($scope.RefundApplicantList.RefundMode == 'Partial_Refund') && ($scope.RefundCancelAmountObj.CalculateAmount > (($scope.RefundCancelAmountObj.AmountPaid) - 1))) {
        //    alert("Refunded amount has been grater than partially deducted amount. Please check...!");
        //}
        else if (($scope.RefundApplicantList.RefundMode == 'Partial_Refund') && (($scope.SavedCalculateAmount > $scope.RefundCancelAmountObj.CalculateAmount) || ($scope.RefundCancelAmountObj.CalculateAmount > (($scope.RefundCancelAmountObj.AmountPaid) - 1)))) {
            alert("Refunded amount should be between partially deducted amount & applicant paid amount. Please check...!");
        }
        //else if ($scope.RefundCancelAmountObj.AcademicRemark == null || $scope.RefundCancelAmountObj.AcademicRemark == "" || $scope.RefundCancelAmountObj.AcademicRemark == undefined) {
        //    alert("Please Enter Refunded Remarks..!");
        //}
        else {
        
            $http({
                method: 'POST',
                url: 'api/RefundCaseCancelByAcademic/RefCaseCancelUpdateAcademicRefAmount',
                data: $scope.RefundCancelAmountObj,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        alert(response.obj);
                        $localStorage.BacktoRefundCaseCancelPage = {};
                        $localStorage.BacktoRefundCaseCancelPage.Flag = true;
                        $localStorage.BacktoRefundCaseCancelPage.Id = $scope.RefundApplicantList.FacultyId;
                        $state.go('RefundCaseCancelByAcademic');
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

   
    ////Function for go back to list of Refund Process Page for Cancellation Case
    $scope.backToRefundCaseCancelList = function () {

        $localStorage.BacktoRefundCaseCancelPage = {};
        $localStorage.BacktoRefundCaseCancelPage.Flag = true;
        $localStorage.BacktoRefundCaseCancelPage.Id = $scope.RefundApplicantList.FacultyId;
        $state.go('RefundCaseCancelByAcademic');
            
    };


    if ($localStorage.BacktoRefundCaseCancelPage.Flag == true) {
        $localStorage.BacktoRefundCaseCancelPage.Flag = false;
        $scope.RefundCaseCancel.Id = $localStorage.BacktoRefundCaseCancelPage.Id;
        $scope.getFacultyById($scope.RefundCaseCancel.Id);
        $scope.RefCaseCancelAcademicApplicantsList();
    }
    else {
        $localStorage.BacktoRefundCaseCancelPage = {};
    }

    //Excel Students Refund Case Report for Academic Section
    $scope.ExportAcademicCancelRefundDatatoExcel = function () {

        //for (var i in $scope.ApplicantListCancelforAcademic) {
        //    if ($scope.ApplicantListCancelforAcademic[i].IsApprovedByAcademic == true) {
        //        $scope.ApplicantListCancelforAcademic[i].IsApprovedByAcademic = 'Approved';
        //    }
        //    else if ($scope.ApplicantListCancelforAcademic[i].IsApprovedByAcademic == false) {
        //        $scope.ApplicantListCancelforAcademic[i].IsApprovedByAcademic = 'Rejected';
        //    }
        //    else if ($scope.ApplicantListCancelforAcademic[i].IsApprovedByAcademic == null) {
        //        $scope.ApplicantListCancelforAcademic[i].IsApprovedByAcademic = 'Pending';
        //    }
        //}
        //for (var i in $scope.ApplicantListCancelforAcademic) {

        //    if ($scope.ApplicantListCancelforAcademic[i].ApprovedOnAcademic == "" || $scope.ApplicantListCancelforAcademic[i].ApprovedOnAcademic == null) {
        //        $scope.ApplicantListCancelforAcademic[i].ApprovedOnAcademic = 'NA';
        //    }
        //}

        for (var i in $scope.ApplicantListCancelforAcademic) {

            if ($scope.ApplicantListCancelforAcademic[i].RequestStatus == "" || $scope.ApplicantListCancelforAcademic[i].RequestStatus == null) {
                $scope.ApplicantListCancelforAcademic[i].RequestStatus = '--';
            }
        }
        for (var i in $scope.ApplicantListCancelforAcademic) {

            if ($scope.ApplicantListCancelforAcademic[i].AmountPaid == "" || $scope.ApplicantListCancelforAcademic[i].AmountPaid == null) {
                $scope.ApplicantListCancelforAcademic[i].AmountPaid = '--';
            }
        }
        for (var i in $scope.ApplicantListCancelforAcademic) {

            if ($scope.ApplicantListCancelforAcademic[i].RefundAmountByAcademic == "" || $scope.ApplicantListCancelforAcademic[i].RefundAmountByAcademic == null) {
                $scope.ApplicantListCancelforAcademic[i].RefundAmountByAcademic = '--';
            }
        }
        for (var i in $scope.ApplicantListCancelforAcademic) {

            if ($scope.ApplicantListCancelforAcademic[i].RefundAmountByAudit == "" || $scope.ApplicantListCancelforAcademic[i].RefundAmountByAudit == null) {
                $scope.ApplicantListCancelforAcademic[i].RefundAmountByAudit = '--';
            }
        }
        alert("Please wait, Excel is being prepared...");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "Cancel_Admission_Refund_Report_for_Academic" + DateWithoutDashed + time;

        var mystyleSecond = {
            headers: true,
            style: 'font-size:25px;font-weight:bold;',
            caption: {
                title: 'Cancelled Admission Refund Case Report for Academic on ' + DateAndTime,
            },
            caption: {
                title: 'Cancelled Admission Refund Case Report for Academic on ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr No.' },
                { columnid: 'ApplicationId', title: 'Application Id' },
                { columnid: 'FullName', title: 'Full Name' },
                { columnid: 'InstancePartTermName', title: 'Programme Name' },
                { columnid: 'RequestStatus', title: 'Refund Status' },
                { columnid: 'AmountPaid', title: 'Applicant Paid Amount' },
                { columnid: 'RefundAmountByAcademic', title: 'Academic Refunded Amount' },
                { columnid: 'RefundAmountByAudit', title: 'Audit Refunded Amount' },
                { columnid: 'IsApprovedByAcademic', title: 'Academic Approval Status' },
                { columnid: 'ApprovedOnAcademic', title: 'Academic Approved On' },
                { columnid: 'IsApprovedByAudit', title: 'Audit Approval Status' },
                { columnid: 'ProcessedOnAudit', title: 'Audit Approved On' },
                { columnid: 'IsApprovedByAccount', title: 'Account Approval Status' },
                { columnid: 'ProcessedOnAccount', title: 'Account Approved On' },
                //{ columnid: 'RefundAmountByAcademic', title: 'Refund Amount By Academic' },
                //{ columnid: 'RefundAmountByAudit', title: 'Refund Amount By Audit' },

            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyleSecond, $scope.ApplicantListCancelforAcademic]);
    };
});



