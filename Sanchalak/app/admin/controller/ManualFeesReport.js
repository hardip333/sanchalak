app.controller('ManualFeesReportCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, $localStorage, NgTableParams) {
    
    /*Variable Declration */
   
    $scope.ManualFeesReport = {};
  
    $scope.ApplicantDetailsVisible = false;
    $rootScope.pageTitle = "Manage Manual Fees Report";
   

  
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
   
    $scope.getManualFeesReport = function () {

        
        $http({
            method: 'POST',
            url: 'api/ManualEntryOfFee/ManualFeesReport',
            data: { AcademicYearId: $scope.ManualFeesReport.AcademicYearId },
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
                        $scope.ManualFeesReportTableParams = new NgTableParams({
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
                    $scope.ManualFeesReportTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                    $scope.exportDataFull = response.obj;
                    $scope.ApplicantDetailsVisible = true;
                }
                
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.AdmissionRecieptList = function (data) {
        
        $localStorage.AppObject = {};
        $localStorage.AppObject.AppId = data.ApplicationFormNo;
        $state.go('AdmissionFeesPaidReceiptList');
    }

    

    
    

   


    

    

    



});
