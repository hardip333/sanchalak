app.controller('MstChoiceTypeController', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

   
    $rootScope.pageTitle = "Manage Choice Type";

    $scope.MstChoiceType = {};

    /*Reset Choice*/
    $scope.MstChoiceTypeReset = function () {
        $scope.data = {};
    };

    
    

    /*Add New Choice*/
    $scope.newMstChoiceTypeAdd = function () {
        $state.go('MstChoiceTypeAdd');
    };

    /*Back to Edit Page of Choice*/
    $scope.backToList = function () {
        $state.go('MstChoiceTypeEdit');
    };

    // Choice Get*/
    $scope.MstChoiceTypeGet = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstChoiceType/MstChoiceTypeGet',

            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    debugger
                    $scope.ChoiceTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                    //$scope.SpecialisationData = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Display Choice Data*/
    $scope.displayChoice = function (data) {
        $scope.MstChoiceType = data;
    };

    /* Choice Add*/
    $scope.MstChoiceTypeAdd = function () {

       // debugger;
        $http({
            method: 'POST',
            url: 'api/MstChoiceType/MstChoiceTypeAdd',
            data: $scope.MstChoiceType,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    //debugger;
                    alert(response.obj);
                    $scope.MstChoiceType = {};
                    $scope.MstChoiceTypeGet();
                    $state.go('MstChoiceTypeEdit');
                }

            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    }

    /*Modify Choice Data*/
    $scope.modifyMstChoiceType = function (data) {
        $scope.showFormFlag = true;
        $scope.MstChoiceType = data;
        $(window).scrollTop(0);
    };

    /* Choice Update*/
    $scope.MstChoiceTypeEdit = function () {

        $http({
            method: 'POST',
            url: 'api/MstChoiceType/MstChoiceTypeEdit',
            data: $scope.MstChoiceType,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {                   
                    alert(response.obj);
                    $scope.MstChoiceTypeGet();
                    $scope.showFormFlag = false;
                }

            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    }

   
    /* Choice Delete*/
    $scope.MstChoiceTypeDelete = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.MstChoiceType = data;

     
        $http({
            method: 'POST',
            url: 'api/MstChoiceType/MstChoiceTypeDelete',
            data: $scope.MstChoiceType,
            headers: { "Content-Type": 'application/json' }
        })
                .success(function (response) {

                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.MstChoiceTypeGet();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });

        }, function () {
            $scope.status = 'You decided to keep your debt.';
        });
    }

    /*Active Enable Choice Type*/
    $scope.ShowChoice = function (data) {

        $scope.MstChoiceType = data;
        $http({
            method: 'POST',
            url: 'api/MstChoiceType/MstChoiceIsActive',
            data: $scope.MstChoiceType,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                 
                    alert(response.obj);
                    $scope.MstChoiceTypeGet();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Active Disable Choice Type*/
    $scope.HideChoice = function (data) {

        $scope.MstChoiceType = data;
      
        $http({
            method: 'POST',
            url: 'api/MstChoiceType/MstChoiceIsSuspended',
            data: $scope.MstChoiceType,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                
                else {debugger;
                    alert(response.obj);
                    $scope.MstChoiceTypeGet();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

});

