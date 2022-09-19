app.controller('PaymentDetailsCtrl', function ($scope, $stateParams, $http, $filter, $localStorage, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Payment Details";

    $scope.PaymentDetails = {}

    $scope.PaymentDetailsGet = function () {
        //alert("OK");
        var data = new Object();
        console.log($stateParams)
        
        
        $http({
            method: 'POST',
            url: 'api/PaymentDetails/AdmApplicationGet',
            //data: data,
            data: { Id: $stateParams.obj },
                    headers: { "Content-Type": 'application/json' }
                })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        $scope.PaymentDetailsGetTableParams = new NgTableParams({
                        }, {
                            dataset: response.obj


                        });
                    }
                    //console.log(dataset);
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);

                });
             

    }
    $scope.PaymentDetailsGet();

    $scope.backToList = function () {
        $state.go('ApplicationPaymentReport');
    }; 

});

