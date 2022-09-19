﻿app.controller('ManualEntryOfFeeCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, $localStorage, NgTableParams) {
    var tokenCookie = $cookies.get('token');
    if (!tokenCookie) {
        localStorage.clear();
        $state.go('login');
    }
    /*Variable Declration */
   
    $scope.FEECONFIG = {};
  
    
    $rootScope.pageTitle = "Manage Update Transaction Details";
   

  
    /*Academic Year List Get Method */
    $scope.IncAcademicYearListGet = function () {
        //alert("Faculty Details");
        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/AcademicYearGet',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    if (response.response_code == "201") {
                        $scope.AcademicList = {};

                    }
                }
                else {
                    $scope.AcademicList = response.obj;
                    $scope.showMatrixFlag = false;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };
    /* Faculty List Get Method*/
    $scope.FacultyGet = function () {
        $scope.FacultyList = {};
        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/FacultyGet',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    if (response.response_code == "201") {
                        $scope.FacultyList = {};

                    }
                }
                else {
                    $scope.FacultyList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };
    /* ProgrammeLevel List Get Method*/
    $scope.ProgrammeLavelGet = function () {
        $scope.PLList = {};
        $http({
            method: 'POST',
            url: 'api/MstProgrammeLevel/MstProgrammeLevelListGet',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    if (response.response_code == "201") {
                        $scope.PLList = {};

                    }
                }
                else {
                    $scope.PLList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };
    /*Programme Instance List By Academic Year Id and Faculty Id */
    $scope.getProgrammeInstanceListByAcadId = function () {
        $scope.InstList = {};
        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/InstanceListGetbyFacultyIdAndAcadId',
            data: $scope.FEECONFIG,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    if (response.response_code == "201") {
                        $scope.InstList = {};

                    }
                }
                else {
                    $scope.InstList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };
    /*Branch/Specialisation List By Programme Instance Id */
    $scope.getBranchListByProgInstId = function () {
        $scope.BranchList = {};

        $scope.FEECONFIG.Id = $scope.FEECONFIG.ProgrammeInstanceId;
        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/MstProgrammeBranchListGetByProgInstanceId',
            data: $scope.FEECONFIG,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    if (response.response_code == "201") {
                        $scope.BranchList = {};

                    }
                }
                else {
                    $scope.BranchList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    /*Programme Part List By Programme Instance Id */
    $scope.getProgrammePartListByProgInstId = function () {
        //$scope.FEECONFIG.ProgrammeInstanceId = $scope.FEECONFIG.ProgrammeInstanceId;


        $scope.ProgPartList = {};
        $http({
            method: 'POST',
            url: 'api/ProgrammeInstancePart/MstProgrammePartGetByProgrammeIdAndProgInstId',
            data: $scope.FEECONFIG,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    if (response.response_code == "201") {
                        $scope.ProgPartList = {};

                    }
                }
                else {
                    $scope.ProgPartList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    /*Programme Part Term List By Programme Instance Id */
    $scope.getProgPartTermListByProgInstPartId = function () {
        $scope.ProgPartTermList = {};
        $http({
            method: 'POST',
            url: 'api/ProgrammeInstancePart/ProgrammePartTermGetByProgInstId',
            data: $scope.FEECONFIG,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    if (response.response_code == "201") {
                        $scope.ProgPartTermList = {};

                    }
                }
                else {
                    $scope.ProgPartTermList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getManualEntryOfFeeByProgInstPartTermId = function () {

        $http({
            method: 'POST',
            url: 'api/ManualEntryOfFee/ManualEntryOfFeeDetailsByProgInsPartTermId',
            data: $scope.FEECONFIG,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                if (response.response_code == "0") {
                    $state.go('login');
                }
                else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);

                    if (response.obj == "No Record Found") {

                        $scope.NoRecLabel = true;
                        $scope.ApplicantDetailsVisible = false;




                    }
                }

                else {
                    $scope.NoRecLabel = false;
                    $scope.ApplicantDetailsVisible = true;
                    $scope.ManualEntryOfFeeTableParams = new NgTableParams({
                    }, {

                        dataset: response.obj

                    });
                    $scope.ManualEntryOfFee = response.obj;
                  
                }
               
               
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.GetFeeDetails = function (obj) {
        
        $scope.FeeDetails = obj;
        $scope.UpdateTransactionDetails = true;
      
    };


    $scope.updateFeeDetails = function (obj) {
        
        $scope.FeeUpdateDetails.ApplicationFormNo = obj.ApplicationFormNo;
        $scope.FeeUpdateDetails.TotalAmount = obj.TotalAmount;
        $scope.FeeUpdateDetails.FeeCategoryId = obj.FeeCategoryId;
        $scope.FeeUpdateDetails.FeeCategoryPartTermMapId = obj.FeeCategoryPartTermMapId;
        $scope.FeeUpdateDetails.PaymentStatus = obj.PaymentStatus;
        $scope.FeeUpdateDetails.TransactionStatus = obj.TransactionStatus;
        $scope.FeeUpdateDetails.ProgrammeInstancePartTermId = obj.ProgrammeInstancePartTermId;
       
        var flag = true;
        if ($scope.FeeUpdateDetails.TotalAmountPaid === null || $scope.FeeUpdateDetails.TotalAmountPaid === undefined ||
            $scope.FeeUpdateDetails.UniversityAccountNumber === null || $scope.FeeUpdateDetails.UniversityAccountNumber === undefined ||
            $scope.FeeUpdateDetails.TransactionId === null || $scope.FeeUpdateDetails.TransactionId===undefined)
        {
            alert("Please Enter All The Required fields");
            flag = false;

        }
       
        else if ($scope.FeeUpdateDetails.TotalAmount != $scope.FeeUpdateDetails.TotalAmountPaid) {
            alert("Total Amount And Received Amount Should be same");
            $scope.FeeUpdateDetails.TotalAmountPaid = {};
           flag= false;
        }
       
      
        if (flag == true) {
        
            
        $http({
            method: 'POST',
            url: 'api/ManualEntryOfFee/UpdateAdmissionFeesICCR',
            data: $scope.FeeUpdateDetails,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    alert(response.obj);
                }
               
                else {
                    $mdDialog.show(
                        $mdDialog.alert()
                            .parent(angular.element(document.querySelector('#popupContainer')))
                            .clickOutsideToClose(true)
                            .title("Message")
                            .textContent("Transaction has been Added!")
                            .ariaLabel('Alert Dialog Demo')
                            .ok('Okay!')
                    );
                    $scope.getManualEntryOfFeeByProgInstPartTermId();
                    $scope.UpdateTransactionDetails = false;
                    $scope.ApplicantDetailsVisible = true;

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

        }
    };

    $scope.resetForm = function () {
        $scope.FeeUpdateDetails = {};
       
    };

   


    $scope.AdmAdmissionFeesPaidRecieptGet = function () {

        $scope.AdmissionFeesPaidReciept = {};
        $scope.AdmissionFeesPaidReciept.Id = $localStorage.AppObject.AppId;
    
        $http({
            method: 'POST',
            url: 'api/MstStudentAdmissionFee/AdmAdmissionFeesPaidRecieptGet',
            data: $scope.AdmissionFeesPaidReciept,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.AdmAdmissionFeesPaidRecieptGetTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    }

    $scope.AdmissionRecieptList = function (data) {
       
        $localStorage.AppObject = {};
        $localStorage.AppObject.AppId = data.ApplicationFormNo;      
        $state.go('AdmissionFeesPaidReceiptList');
    }

    $scope.AdmissionReciept = function (ApplicationId, OrderId) {
        
        $localStorage.AppObject = {};
        $localStorage.AppObject.AppId = ApplicationId;
        $localStorage.AppObject.OrderId = OrderId;
        $state.go('AdmissionFeesPaidReceipt');
    }

    $scope.AdmAdmissionFeesPaidRecieptDetailGet = function () {
        $scope.AdmissionFeesPaidReciept = {};
        $scope.AdmissionFeesPaidReciept.Id = $localStorage.AppObject.AppId;
        $scope.AdmissionFeesPaidReciept.OrderId = $localStorage.AppObject.OrderId;
        $http({
            method: 'POST',
            url: 'api/MstStudentAdmissionFee/FeesPaidRecieptDetailGet',
            data: $scope.AdmissionFeesPaidReciept,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.FeeReceiept = response.obj[0];
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);

            });
    }

    



});
