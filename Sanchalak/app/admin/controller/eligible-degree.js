

app.controller('eligibledegreeCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Degree";

    $scope.btnAdd = true;
    $scope.btnUpdate = false;
  
    $scope.getDegreeList = function () {

        var data = new Object();
        //data.id = $rootScope.id;

        $http({
            method: 'POST',
            url: 'api/AdmEligibleDegree/getAdmEligibleDegreeList',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.degreeTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    }
    $scope.getDegreeList();


    $scope.addDegree = function () {

        $http({
            method: 'POST',
            url: 'api/AdmEligibleDegree/insertAdmEligibleDegreeData',
            data: $scope.ed1,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error");
                }
                else {
                    alert(response.obj);
                    $scope.ed1 = {};
                    $scope.getDegreeList();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.updateDegree = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/AdmEligibleDegree/updateAdmEligibleDegreeData',
            data: $scope.ed1,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.ed1 = {};
                    $scope.getDegreeList();
                    $scope.btnAdd = true;
                    $scope.btnUpdate = false;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.deleteDegree = function (data) {

        $scope.ed1 = data;

        $http({
            method: 'POST',
            url: 'api/AdmEligibleDegree/deleteAdmEligibleDegreeData',
            data: $scope.ed1,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.ed1 = {};
                    $scope.getDegreeList();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

        
    $scope.cancelDegree = function () {
        $scope.ed1 = {};
        $scope.getDegreeList();
        $scope.modifyUserFlag = false;
    }

    $scope.modifyDegree = function (data) {
        $scope.ed1 = data;
        $scope.modifyUserFlag = true;
        $scope.btnAdd = false;
        $scope.btnUpdate = true;
      
    }
});