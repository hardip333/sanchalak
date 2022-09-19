app.controller('RefundCaseByAcademicCtrl', function ($scope, $http, $rootScope, $localStorage, $filter, $state, $cookies, $mdDialog, NgTableParams) {
    $rootScope.pageTitle = "Refund Process by Academic";
   
    $scope.RefundCase = {};
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
            url: 'api/MstFaculty/MstFacultyGet',
            data: $scope.RefundCase,
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
    $scope.RefCaseAcademicApplicantsList = function () {
        $scope.checkDataExists = false;
        //var FacultyId = { FacultyId: $scope.RefundCase.Id };
        $scope.RefundCase.FacultyId = $scope.RefundCase.Id;

        if ($scope.RefundCase.Id == null || $scope.RefundCase.Id == "" || $scope.RefundCase.Id === undefined) {
            alert("Please select Faculty...!");
        }
        else {
            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/RefundCaseByAcademic/RefCaseAcademicApplicantsList',
                data: $scope.RefundCase,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    // $scope.ApplicantByProgPartTermList = response.obj;
                    $scope.RefundCaseTableparam = new NgTableParams({

                    },
                        { dataset: response.obj });
                    $scope.ApplicantListforAcademic = response.obj;
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
        $localStorage.RefundCaseAppId = AppId;
        $localStorage.RequestType = RequestType;
        $state.go('RefundCaseApplicantDetails');
    };

    $scope.cancelRefundCaseList = function () {
        $scope.RefundCase = {};
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
                $scope.RefundAmountObj.CalculateAmount = ($scope.RefundApplicantList.AmounttoRefund) - 1000;
                $scope.SavedCalculateAmount = $scope.RefundAmountObj.CalculateAmount;
            }
            else {
                $scope.RefundAmountObj.CalculateAmount = ($scope.RefundApplicantList.AmounttoRefund) - ($scope.PercentageAmount);
                $scope.SavedCalculateAmount = $scope.RefundAmountObj.CalculateAmount;
            }
            $scope.RefundAmountText = false;
        }
        else {
            $scope.RefundAmountObj.CalculateAmount = $scope.RefundApplicantList.AmounttoRefund;
            $scope.RefundAmountText = true;
        }

    };

    //Function for get Applicant list Details for Refund Process by Faculty
    $scope.RefCaseAcademicApplicantsDetails = function () {

        $http({
            method: 'POST',
            url: 'api/RefundCaseByAcademic/RefCaseAcademicApplicantsDetails',
            data: {
                ApplicationId: $localStorage.RefundCaseAppId,
                RequestType: $localStorage.RequestType
            },
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

    //Function for update Refund Amount
    $scope.RefCaseUpdateAcademicRefAmount = function () {

        $scope.RefundAmountObj.ApplicationId = $scope.RefundApplicantList.ApplicationId;
        $scope.RefundAmountObj.RefundAmountByAcademic = $scope.RefundAmountObj.CalculateAmount;
        $scope.RefundAmountObj.AmountPaid = $scope.RefundApplicantList.AmountPaid;
        $scope.RefundAmountObj.RequestType = $localStorage.RequestType;
        $scope.RefundAmountObj.RefundMode = $scope.RefundApplicantList.RefundMode;
        $scope.RefundAmountObj.AutoDeductAmount = $scope.SavedCalculateAmount;

        if ($scope.RefundAmountObj.RefundAmountByAcademic == null || $scope.RefundAmountObj.RefundAmountByAcademic == "" || $scope.RefundAmountObj.RefundAmountByAcademic == undefined ) {
            alert("Please Enter Refunded Amount..!");
        }
        else if (($scope.RefundApplicantList.RefundMode == 'Full_Refund') && ($scope.RefundAmountObj.RefundAmountByAcademic != $scope.RefundAmountObj.AmountPaid)) {
            alert("Refunded amount should be same as applicant paid amount. Please check...!");
        }
        //else if (($scope.RefundApplicantList.RefundMode == 'Partial_Refund') && ($scope.SavedCalculateAmount > (($scope.RefundAmountObj.AmountPaid) - 1))) {
        //    alert("Refunded amount has been grater than partially deducted amount. Please check...!");
        //}
        else if (($scope.RefundApplicantList.RefundMode == 'Partial_Refund') && (($scope.SavedCalculateAmount > $scope.RefundAmountObj.CalculateAmount) || ($scope.RefundAmountObj.CalculateAmount > (($scope.RefundAmountObj.AmountPaid)-1)))) {
            alert("Refunded amount should be between partially deducted amount & applicant paid amount. Please check...!");
        }
        //else if ($scope.RefundAmountObj.RefundAmountByAcademic > $scope.RefundAmountObj.AmountPaid) {
        //    alert("Refunded amount has been grater than applicant paid amount. Please check...!");
        //}
        //else if ($scope.RefundAmountObj.AcademicRemark == null || $scope.RefundAmountObj.AcademicRemark == "" || $scope.RefundAmountObj.AcademicRemark == undefined) {
        //    alert("Please Enter Refunded Remarks..!");
        //}
        else {
        
            $http({
                method: 'POST',
                url: 'api/RefundCaseByAcademic/RefCaseUpdateAcademicRefAmount',
                data: $scope.RefundAmountObj,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        alert(response.obj);
                        $localStorage.BacktoRefundCasePage = {};
                        $localStorage.BacktoRefundCasePage.Flag = true;
                        $localStorage.BacktoRefundCasePage.Id = $scope.RefundApplicantList.FacultyId;
                        $state.go('RefundCaseByAcademic');
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

   
    //Function for go back to list of Refund Process Page
    $scope.backToRefundCaseList = function () {

        $localStorage.BacktoRefundCasePage = {};
        $localStorage.BacktoRefundCasePage.Flag = true;
        $localStorage.BacktoRefundCasePage.Id = $scope.RefundApplicantList.FacultyId;
        $state.go('RefundCaseByAcademic');
            
    };


    if ($localStorage.BacktoRefundCasePage.Flag == true) {
        $localStorage.BacktoRefundCasePage.Flag = false;
        $scope.RefundCase.Id = $localStorage.BacktoRefundCasePage.Id;
        $scope.getFacultyById($scope.RefundCase.Id);
        $scope.RefCaseAcademicApplicantsList();
    }
    else {
        $localStorage.BacktoRefundCasePage = {};
    }


    //Excel Students Refund Case Report for Academic Section
    $scope.ExportAcademicRefundDatatoExcel = function () {
       
        //for (var i in $scope.ApplicantListforAcademic) {
        //    if ($scope.ApplicantListforAcademic[i].IsApprovedByAcademic == true) {
        //        $scope.ApplicantListforAcademic[i].IsApprovedByAcademic = 'Approved';
        //    }
        //    else if ($scope.ApplicantListforAcademic[i].IsApprovedByAcademic == false) {
        //        $scope.ApplicantListforAcademic[i].IsApprovedByAcademic = 'Rejected';
        //    }
        //    else if ($scope.ApplicantListforAcademic[i].IsApprovedByAcademic == null) {
        //        $scope.ApplicantListforAcademic[i].IsApprovedByAcademic = 'Pending';
        //    }
        //}

        for (var i in $scope.ApplicantListforAcademic) {

            if ($scope.ApplicantListforAcademic[i].RequestStatus == "" || $scope.ApplicantListforAcademic[i].RequestStatus == null) {
                $scope.ApplicantListforAcademic[i].RequestStatus = '--';
            }
        }
        for (var i in $scope.ApplicantListforAcademic) {
            
            if ($scope.ApplicantListforAcademic[i].AmountPaid == "" || $scope.ApplicantListforAcademic[i].AmountPaid == null) {
                $scope.ApplicantListforAcademic[i].AmountPaid = '--';
            }
        }
        for (var i in $scope.ApplicantListforAcademic) {

            if ($scope.ApplicantListforAcademic[i].RefundAmountByAcademic == "" || $scope.ApplicantListforAcademic[i].RefundAmountByAcademic == null) {
                $scope.ApplicantListforAcademic[i].RefundAmountByAcademic = '--';
            }
        }
        for (var i in $scope.ApplicantListforAcademic) {

            if ($scope.ApplicantListforAcademic[i].RefundAmountByAudit == "" || $scope.ApplicantListforAcademic[i].RefundAmountByAudit == null) {
                $scope.ApplicantListforAcademic[i].RefundAmountByAudit = '--';
            }
        }
        

        alert("Please wait, Excel is being prepared...");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "Refund_Report_for_Academic" + DateWithoutDashed + time;

        var mystyleSecond = {
            headers: true,
            style: 'font-size:25px;font-weight:bold;',
            caption: {
                title: 'Refund Case Report for Academic on ' + DateAndTime,
            },
            caption: {
                title: 'Refund Case Report for Academic on ' + DateAndTime,
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
                { columnid: 'IsApprovedByAcademic', title: 'Academic Approval Status' },
                { columnid: 'ProcessedOnAcademic', title: 'Academic Approved On' },
                { columnid: 'IsApprovedByAudit', title: 'Audit Approval Status' },
                { columnid: 'ProcessedOnAudit', title: 'Audit Approved On' },
                { columnid: 'IsApprovedByAccount', title: 'Account Approval Status' },
                { columnid: 'ProcessedOnAccount', title: 'Account Approved On' },
                //{ columnid: 'RefundAmountByAcademic', title: 'Refund Amount By Academic' },
                //{ columnid: 'RefundAmountByAudit', title: 'Refund Amount By Audit' },

            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyleSecond, $scope.ApplicantListforAcademic]);
    };

});



