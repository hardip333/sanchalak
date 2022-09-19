app.controller('ExamFormBarcodePdfGenerationRequestCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams, $window, $location) {

    $rootScope.pageTitle = "Manage ExamForm Barcode Pdf Generation Request";

    $rootScope.showLoading = false;

    // for list in table
    $scope.getExamFormBarcodePdfgenRequest = function () {
      
        $rootScope.showLoading = true;
        var xml = new Object();

        $http({
            method: 'POST',
            url: 'api/BlockAllocation/getExamFormBarcodePdfgenRequestList',
            data: xml,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
         
                    $scope.getExamFormBarcodePdfgenRequestListforList = response.obj;
          
                    var data = $scope.getExamFormBarcodePdfgenRequestListforList.slice();

                    $scope.barcodepdfTableParams = new NgTableParams({
                        count: 1000
                    }, {
                        dataset: data
                    });
                 
                }
             
    
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };
    $scope.getExamFormBarcodePdfgenRequest();

   






});