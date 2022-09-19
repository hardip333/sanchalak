app.controller('RefundCaseCancelByAuditCtrl', function ($scope, $http, $rootScope, $localStorage, $filter, $state, $cookies, $mdDialog, NgTableParams) {
    $rootScope.pageTitle = "Refund Process by Audit for Cancel Admission";
   
    $scope.RefundCaseCancelAudit = {};
    $scope.RefundCancelAmountAuditObj = {};
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
            data: $scope.RefundCaseCancelAudit,
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

    //Function for get Applicant list for Refund Process by Faculty for Cancellation
    $scope.RefCaseCancelAuditApplicantsList = function () {

        $scope.checkDataExists = false;
        //var FacultyId = { FacultyId: $scope.RefundCaseCancelAudit.Id };
        $scope.RefundCaseCancelAudit.FacultyId = $scope.RefundCaseCancelAudit.Id;

        if ($scope.RefundCaseCancelAudit.Id == null || $scope.RefundCaseCancelAudit.Id == "" || $scope.RefundCaseCancelAudit.Id === undefined) {
            alert("Please select Faculty...!");
        }
        else {
            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/RefundCaseCancelByAudit/RefCaseCancelAuditApplicantsList',
                data: $scope.RefundCaseCancelAudit,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    // $scope.ApplicantByProgPartTermList = response.obj;
                    $scope.RefundCaseTableparam = new NgTableParams({

                    },
                        { dataset: response.obj });
                    $scope.ApplicantListCancelforAudit = response.obj;
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
        $localStorage.RefundCaseCancelAuditAppId = AppId;
        $state.go('RefundCaseCancelApplicantDetailsByAudit');
    };

    $scope.cancelRefundCaseList = function () {
        $scope.RefundCaseCancelAudit = {};
        $scope.showFormFlag = false;
    };

    //Function for get Applicant list Details for Refund Process by Faculty
    $scope.RefCaseCancelAuditApplicantsDetails = function () {

        $http({
            method: 'POST',
            url: 'api/RefundCaseCancelByAudit/RefCaseCancelAuditApplicantsDetails',
            data: { ApplicationId: $localStorage.RefundCaseCancelAuditAppId },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $scope.RefundApplicantList = response.obj[0];
                $scope.RefundApplicantList.AmountRefundByAudit = $scope.RefundApplicantList.RefundAmountByAcademic;
            })
            .error(function (res) {
                alert(res);
            });

    };

    //Function for update Refund Amount
    $scope.RefCaseCancelUpdateAuditRefAmount = function () {
  
        $scope.RefundCancelAmountAuditObj.ApplicationId = $scope.RefundApplicantList.ApplicationId;
        $scope.RefundCancelAmountAuditObj.AmountRefundByAudit = $scope.RefundApplicantList.AmountRefundByAudit;
        $scope.RefundCancelAmountAuditObj.RefundAmountByAcademic = $scope.RefundApplicantList. RefundAmountByAcademic;


        if ($scope.RefundCancelAmountAuditObj.AmountRefundByAudit == null || $scope.RefundCancelAmountAuditObj.AmountRefundByAudit == "" || $scope.RefundCancelAmountAuditObj.AmountRefundByAudit == undefined ) {
            alert("Please Enter Refunded Amount..!");
        }
        else if ($scope.RefundCancelAmountAuditObj.AmountRefundByAudit > $scope.RefundCancelAmountAuditObj.RefundAmountByAcademic) {
            alert("Refunded amount has been grater than Academic Refunded amount. Please check...!");
        }
        //else if ($scope.RefundCancelAmountAuditObj.AuditRemark == null || $scope.RefundCancelAmountAuditObj.AuditRemark == "" || $scope.RefundCancelAmountAuditObj.AuditRemark == undefined) {
        //    alert("Please Enter Refunded Remarks..!");
        //}
        else {
        
            $http({
                method: 'POST',
                url: 'api/RefundCaseCancelByAudit/RefCaseCancelUpdateAuditRefAmount',
                data: $scope.RefundCancelAmountAuditObj,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        alert(response.obj);
                        $localStorage.BacktoRefundCaseCancelAuditPage = {};
                        $localStorage.BacktoRefundCaseCancelAuditPage.Flag = true;
                        $localStorage.BacktoRefundCaseCancelAuditPage.Id = $scope.RefundApplicantList.FacultyId;
                        $state.go('RefundCaseCancelByAudit');
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

   
    //Function for go back to list of Refund Process Page
    $scope.backToRefundCaseCancelList = function () {

        $localStorage.BacktoRefundCaseCancelAuditPage = {};
        $localStorage.BacktoRefundCaseCancelAuditPage.Flag = true;
        $localStorage.BacktoRefundCaseCancelAuditPage.Id = $scope.RefundApplicantList.FacultyId;
        $state.go('RefundCaseCancelByAudit');
            
    };


    if ($localStorage.BacktoRefundCaseCancelAuditPage.Flag == true) {
        $localStorage.BacktoRefundCaseCancelAuditPage.Flag = false;
        $scope.RefundCaseCancelAudit.Id = $localStorage.BacktoRefundCaseCancelAuditPage.Id;
        $scope.getFacultyById($scope.RefundCaseCancelAudit.Id);
        $scope.RefCaseCancelAuditApplicantsList();
    }
    else {
        $localStorage.BacktoRefundCaseCancelAuditPage = {};
    }


    //Excel Students Refund Case Report for Audit Section
    $scope.ExportAuditCancelRefundDatatoExcel = function () {

        //for (var i in $scope.ApplicantListCancelforAudit) {
        //    if ($scope.ApplicantListCancelforAudit[i].IsApprovedByAudit == true) {
        //        $scope.ApplicantListCancelforAudit[i].IsApprovedByAudit = 'Approved';
        //    }
        //    else if ($scope.ApplicantListCancelforAudit[i].IsApprovedByAudit == false) {
        //        $scope.ApplicantListCancelforAudit[i].IsApprovedByAudit = 'Rejected';
        //    }
        //    else if ($scope.ApplicantListCancelforAudit[i].IsApprovedByAudit == null) {
        //        $scope.ApplicantListCancelforAudit[i].IsApprovedByAudit = 'Pending';
        //    }
        //}

        for (var i in $scope.ApplicantListCancelforAudit) {

            //if ($scope.ApplicantListCancelforAudit[i].ProcessedOnAudit == "" || $scope.ApplicantListCancelforAudit[i].ProcessedOnAudit == null) {
            //    $scope.ApplicantListCancelforAudit[i].ProcessedOnAudit = 'NA';
            //}
            if ($scope.ApplicantListCancelforAudit[i].PassbookDoc == "" || $scope.ApplicantListCancelforAudit[i].PassbookDoc == null) {
                $scope.ApplicantListCancelforAudit[i].PassbookDoc = 'NA';
            }
            if ($scope.ApplicantListCancelforAudit[i].RTGSForm == "" || $scope.ApplicantListCancelforAudit[i].RTGSForm == null) {
                $scope.ApplicantListCancelforAudit[i].RTGSForm = 'NA';
            }
        }
        for (var i in $scope.ApplicantListCancelforAudit) {

             if ($scope.ApplicantListCancelforAudit[i].RequestStatus == "" || $scope.ApplicantListCancelforAudit[i].RequestStatus == null) {
                $scope.ApplicantListCancelforAudit[i].RequestStatus = '--';
            }
        }
        for (var i in $scope.ApplicantListCancelforAudit) {

            if ($scope.ApplicantListCancelforAudit[i].AmountPaid == "" || $scope.ApplicantListCancelforAudit[i].AmountPaid == null) {
                $scope.ApplicantListCancelforAudit[i].AmountPaid = '--';
            }
        }
        for (var i in $scope.ApplicantListCancelforAudit) {

            if ($scope.ApplicantListCancelforAudit[i].RefundAmountByAcademic == "" || $scope.ApplicantListCancelforAudit[i].RefundAmountByAcademic == null) {
                $scope.ApplicantListCancelforAudit[i].RefundAmountByAcademic = '--';
            }
        }
        for (var i in $scope.ApplicantListCancelforAudit) {

            if ($scope.ApplicantListCancelforAudit[i].RefundAmountByAudit == "" || $scope.ApplicantListCancelforAudit[i].RefundAmountByAudit == null) {
                $scope.ApplicantListCancelforAudit[i].RefundAmountByAudit = '--';
            }
        }

        alert("Please wait, Excel is being prepared...");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "Cancel_Admission_Refund_Report_for_Audit" + DateWithoutDashed + time;

        var mystyleSecond = {
            headers: true,
            style: 'font-size:25px;font-weight:bold;',
            caption: {
                title: 'Cancelled Admission Refund Case Report for Audit on  ' + DateAndTime,
            },
            caption: {
                title: 'Cancelled Admission Refund Case Report for Audit on ' + DateAndTime,
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
                { columnid: 'IsApprovedByAudit', title: 'Audit Approval Status' },
                { columnid: 'ProcessedOnAudit', title: 'Audit Approved On' },
                { columnid: 'IsApprovedByAcademic', title: 'Academic Approval Status' },
                { columnid: 'ApprovedOnAcademic', title: 'Academic Approved On' },
                { columnid: 'IsApprovedByAccount', title: 'Account Approval Status' },
                { columnid: 'ProcessedOnAccount', title: 'Account Approved On' },
                { columnid: 'PassbookDoc', title: 'View Passbook Doc' },
                { columnid: 'RTGSForm', title: 'View RTGS Doc' },
                //{ columnid: 'RefundAmountByAcademic', title: 'Refund Amount By Academic' },
                //{ columnid: 'RefundAmountByAudit', title: 'Refund Amount By Audit' },

            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyleSecond, $scope.ApplicantListCancelforAudit]);
    };
});



