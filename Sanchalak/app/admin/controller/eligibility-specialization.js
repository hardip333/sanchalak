

app.controller('eligibilityspecializationCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Specialization";

    $scope.btnAdd = true;
    $scope.btnUpdate = false;

    $scope.getSpecializationList = function () {

        var data = new Object();
        //data.id = $rootScope.id;

        $http({
            method: 'POST',
            url: 'api/EligibilitySpecialization/getEligibilitySpecializationList',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.specializationTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    }
    $scope.getSpecializationList();


    $scope.addSpecialization = function () {
        //if ($scope.esz1 != null || $scope.esz1 != undefined)
        //{
            $http({
                method: 'POST',
                url: 'api/EligibilitySpecialization/insertEligibilitySpecializationData',
                data: $scope.esz1,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error");
                    }
                    else {
                        alert(response.obj);
                        $scope.esz1 = {};
                        $scope.getSpecializationList();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
            
        //}
        //else {
        //    $mdDialog.show(
        //        $mdDialog.alert()
        //            .parent(angular.element(document.querySelector('#popupContainer')))
        //            .clickOutsideToClose(true)
        //            .title("Error")
        //            .textContent("Please complete the form before Submit...")
        //            .ariaLabel('Alert Dialog Demo')
        //            .ok('Okay!')
        //    );
        //}
    };


    $scope.updateSpecialization = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/EligibilitySpecialization/updateEligibilitySpecializationData',
            data: $scope.esz1,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.esz1 = {};
                    $scope.getSpecializationList();
                    $scope.btnAdd = true;
                    $scope.btnUpdate = false;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.deleteSpecialization = function (data) {

        $scope.esz1 = data;
       
        $http({
            method: 'POST',
            url: 'api/EligibilitySpecialization/deleteEligibilitySpecializationData',
            data: $scope.esz1,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.esz1 = {};
                    $scope.getSpecializationList();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    }

        
    $scope.cancelSpecialization = function () {
        $scope.esz1 = {};
        $scope.getSpecializationList();
        $scope.modifyUserFlag = false;
    }

    $scope.modifySpecialization = function (data) {
        $scope.esz1 = data;
        $scope.modifyUserFlag = true;
        $scope.btnAdd = false;
        $scope.btnUpdate = true;
    }
});