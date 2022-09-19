app.controller('RefundCaseByAuditCtrl', function ($scope, $http, $rootScope, $localStorage, $filter, $state, $cookies, $mdDialog, NgTableParams) {
    $rootScope.pageTitle = "Refund Process by Audit";
   
    $scope.RefundCaseAudit = {};
    $scope.RefundAmountAuditObj = {};
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
            data: $scope.RefundCaseAudit,
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

    //Function for get Applicant list for Refund Process by Faculty
    $scope.RefCaseAuditApplicantsList = function () {
        $scope.checkDataExists = false;
        //var FacultyId = { FacultyId: $scope.RefundCaseAudit.Id };
        $scope.RefundCaseAudit.FacultyId = $scope.RefundCaseAudit.Id;

        if ($scope.RefundCaseAudit.Id == null || $scope.RefundCaseAudit.Id == "" || $scope.RefundCaseAudit.Id === undefined) {
            alert("Please select Faculty...!");
        }
        else {
            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/RefundCaseByAudit/RefCaseAuditApplicantsList',
                data: $scope.RefundCaseAudit,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    // $scope.ApplicantByProgPartTermList = response.obj;
                    $scope.RefundCaseTableparam = new NgTableParams({

                    },
                        { dataset: response.obj });
                    $scope.ApplicantListforAudit = response.obj;
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

    $scope.StepToRefundProcess = function (AppId, RequestType) {
        $localStorage.RefundCaseAuditAppId = AppId;
        $localStorage.RequestType = RequestType;
        $state.go('RefundCaseApplicantDetailsByAudit');
    };

    $scope.cancelRefundCaseList = function () {
        $scope.RefundCaseAudit = {};
        $scope.showFormFlag = false;
    };

    //Function for get Applicant list Details for Refund Process by Faculty
    $scope.RefCaseAuditApplicantsDetails = function () {

        $http({
            method: 'POST',
            url: 'api/RefundCaseByAudit/RefCaseAuditApplicantsDetails',
            data: {
                ApplicationId: $localStorage.RefundCaseAuditAppId,
                RequestType: $localStorage.RequestType
            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $scope.RefundApplicantList = response.obj[0];
                console.log($scope.RefundApplicantList);
                $scope.RefundApplicantList.AmountRefundByAudit = $scope.RefundApplicantList.RefundAmountByAcademic;
            })
            .error(function (res) {
                alert(res);
            });

    };

    //Function for update Refund Amount
    $scope.RefCaseUpdateAuditRefAmount = function () {
  
        $scope.RefundAmountAuditObj.ApplicationId = $scope.RefundApplicantList.ApplicationId;
        $scope.RefundAmountAuditObj.AmountRefundByAudit = $scope.RefundApplicantList.AmountRefundByAudit;
        $scope.RefundAmountAuditObj.RefundAmountByAcademic = $scope.RefundApplicantList. RefundAmountByAcademic;
        $scope.RefundAmountAuditObj.RequestType = $localStorage.RequestType;

        if ($scope.RefundAmountAuditObj.AmountRefundByAudit == null || $scope.RefundAmountAuditObj.AmountRefundByAudit == "" || $scope.RefundAmountAuditObj.AmountRefundByAudit == undefined ) {
            alert("Please Enter Refunded Amount..!");
        }
        else if ($scope.RefundAmountAuditObj.AmountRefundByAudit > $scope.RefundAmountAuditObj.RefundAmountByAcademic) {
            alert("Refunded amount has been grater than Academic Refunded amount. Please check...!");
        }
        //else if ($scope.RefundAmountAuditObj.AuditRemark == null || $scope.RefundAmountAuditObj.AuditRemark == "" || $scope.RefundAmountAuditObj.AuditRemark == undefined) {
        //    alert("Please Enter Refunded Remarks..!");
        //}
        else {
        
            $http({
                method: 'POST',
                url: 'api/RefundCaseByAudit/RefCaseUpdateAuditRefAmount',
                data: $scope.RefundAmountAuditObj,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        alert(response.obj);
                        $localStorage.BacktoRefundCaseAuditPage = {};
                        $localStorage.BacktoRefundCaseAuditPage.Flag = true;
                        $localStorage.BacktoRefundCaseAuditPage.Id = $scope.RefundApplicantList.FacultyId;
                        $state.go('RefundCaseByAudit');
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

   
    //Function for go back to list of Refund Process Page
    $scope.backToRefundCaseList = function () {

        $localStorage.BacktoRefundCaseAuditPage = {};
        $localStorage.BacktoRefundCaseAuditPage.Flag = true;
        $localStorage.BacktoRefundCaseAuditPage.Id = $scope.RefundApplicantList.FacultyId;
        $state.go('RefundCaseByAudit');
            
    };


    if ($localStorage.BacktoRefundCaseAuditPage.Flag == true) {
        $localStorage.BacktoRefundCaseAuditPage.Flag = false;
        $scope.RefundCaseAudit.Id = $localStorage.BacktoRefundCaseAuditPage.Id;
        $scope.getFacultyById($scope.RefundCaseAudit.Id);
        $scope.RefCaseAuditApplicantsList();
    }
    else {
        $localStorage.BacktoRefundCaseAuditPage = {};
    }


    //Excel Students Refund Case Report for Audit Section
    $scope.ExportAuditRefundDatatoExcel = function () {

        //for (var i in $scope.ApplicantListforAudit) {
        //    if ($scope.ApplicantListforAudit[i].IsApprovedByAudit == true) {
        //        $scope.ApplicantListforAudit[i].IsApprovedByAudit = 'Approved';
        //    }
        //    else if ($scope.ApplicantListforAudit[i].IsApprovedByAudit == false) {
        //        $scope.ApplicantListforAudit[i].IsApprovedByAudit = 'Rejected';
        //    }
        //    else if ($scope.ApplicantListforAudit[i].IsApprovedByAudit == null) {
        //        $scope.ApplicantListforAudit[i].IsApprovedByAudit = 'Pending';
        //    }
        //}
        for (var i in $scope.ApplicantListforAudit) {

            //if ($scope.ApplicantListforAudit[i].ProcessedOnAudit == "" || $scope.ApplicantListforAudit[i].ProcessedOnAudit == null) {
            //    $scope.ApplicantListforAudit[i].ProcessedOnAudit = 'NA';
            //}
            if ($scope.ApplicantListforAudit[i].PassbookDoc == "" || $scope.ApplicantListforAudit[i].PassbookDoc == null) {
                $scope.ApplicantListforAudit[i].PassbookDoc = 'NA';
            }
            if ($scope.ApplicantListforAudit[i].RTGSForm == "" || $scope.ApplicantListforAudit[i].RTGSForm == null) {
                $scope.ApplicantListforAudit[i].RTGSForm = 'NA';
            }
        }

        for (var i in $scope.ApplicantListforAudit) {

            if ($scope.ApplicantListforAudit[i].RequestStatus == "" || $scope.ApplicantListforAudit[i].RequestStatus == null) {
                $scope.ApplicantListforAudit[i].RequestStatus = '--';
            }
        }
        for (var i in $scope.ApplicantListforAudit) {

            if ($scope.ApplicantListforAudit[i].AmountPaid == "" || $scope.ApplicantListforAudit[i].AmountPaid == null) {
                $scope.ApplicantListforAudit[i].AmountPaid = '--';
            }
        }
        for (var i in $scope.ApplicantListforAudit) {

            if ($scope.ApplicantListforAudit[i].RefundAmountByAcademic == "" || $scope.ApplicantListforAudit[i].RefundAmountByAcademic == null) {
                $scope.ApplicantListforAudit[i].RefundAmountByAcademic = '--';
            }
        }
        for (var i in $scope.ApplicantListforAudit) {

            if ($scope.ApplicantListforAudit[i].RefundAmountByAudit == "" || $scope.ApplicantListforAudit[i].RefundAmountByAudit == null) {
                $scope.ApplicantListforAudit[i].RefundAmountByAudit = '--';
            }
        }

        alert("Please wait, Excel is being prepared...");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "Refund_Report_for_Audit" + DateWithoutDashed + time;

        var mystyleSecond = {
            headers: true,
            style: 'font-size:25px;font-weight:bold;',
            caption: {
                title: 'Refund Case Report for Audit on ' + DateAndTime,
            },
            caption: {
                title: 'Refund Case Report for Audit on ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr No.' },
                { columnid: 'ApplicationId', title: 'Application Id' },
                { columnid: 'FullName', title: 'Full Name' },
                { columnid: 'InstancePartTermName', title: 'Programme Name' },
                { columnid: 'RequestType', title: 'Reason for Refund' },
                { columnid: 'RequestStatus', title: 'Refund Status' },
                { columnid: 'AmountPaid', title: 'Applicant Paid Amount' },
                { columnid: 'RefundAmountByAcademic', title: 'Academic Refunded Amount' },
                { columnid: 'RefundAmountByAudit', title: 'Audit Refunded Amount' },
                { columnid: 'IsApprovedByAudit', title: 'Audit Approval Status' },
                { columnid: 'ProcessedOnAudit', title: 'Audit Approved On' },
                { columnid: 'IsApprovedByAcademic', title: 'Academic Approval Status' },
                { columnid: 'ProcessedOnAcademic', title: 'Academic Approved On' },
                { columnid: 'IsApprovedByAccount', title: 'Account Approval Status' },
                { columnid: 'ProcessedOnAccount', title: 'Account Approved On' },
                { columnid: 'PassbookDoc', title: 'View Passbook Doc' },
                { columnid: 'RTGSForm', title: 'View RTGS Doc' },

                //{ columnid: 'RefundAmountByAcademic', title: 'Refund Amount By Academic' },
                //{ columnid: 'RefundAmountByAudit', title: 'Refund Amount By Audit' },

            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyleSecond, $scope.ApplicantListforAudit]);
    };
});



