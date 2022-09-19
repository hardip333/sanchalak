app.controller('MstUniversitySectionCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage University Section";

    /*Reset University Section*/
    $scope.resetUS = function () {
        $scope.US = {};
    };

    /*Get University Section List*/
    $scope.getUSList = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstUniversitySection/MstUniversitySectionGet',

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
                    $scope.MstUniversitySectionTableParams = new NgTableParams({}, { dataset: response.obj });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Add University Section*/
    $scope.addUS = function () {

        if ($scope.US.UniversitySectionName === null || $scope.US.UniversitySectionName === undefined ||
            $scope.US.UniversitySectionCode === null || $scope.US.UniversitySectionCode === undefined)
         {

            alert("please check faculty name and code fields !!!");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstUniversitySection/MstUniversitySectionAdd',
                data: $scope.US,
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
                        $scope.getUSList();
                        $scope.US = {};

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Modify Faculty Data*/
    $scope.modifyUSData = function (data) {
        $scope.showFormFlag = true;
        $scope.US = data;
        $(window).scrollTop(0);
    };

    /*Display Faculty Data*/
    $scope.displayUS = function (data) {
        $scope.US = data;
    };

    /*Update Faculty*/
    $scope.editUS = function () {

        if ($scope.US.UniversitySectionName === null || $scope.US.UniversitySectionName === undefined ||
            $scope.US.UniversitySectionCode === null || $scope.US.UniversitySectionCode === undefined)
        {

            alert("Please check University Section name and code fields!!!");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstUniversitySection/MstUniversitySectionEdit',
                data: $scope.US,
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
                        $scope.resetUS();
                        $scope.getUSList();
                        $scope.showFormFlag = false;
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Delete Faculty*/
    $scope.deleteUS = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.US = data;

            $http({
                method: 'POST',
                url: 'api/MstUniversitySection/MstUniversitySectionDelete',
                data: $scope.US,
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
                        $scope.getUSList();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
            
                  },
            function () {
            $scope.status = 'You decided to keep your debt.';
        });
    };

    /*Active Enable Faculty*/
    $scope.ShowUS = function (data) {

        $scope.newUS = data;

        $http({
            method: 'POST',
            url: 'api/MstUniversitySection/MstUniversitySectionActive',
            data: $scope.newUS,
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
                    $scope.getUSList();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Active Disable Faculty*/
    $scope.HideUS = function (data) {

        $scope.newUS = data;

        $http({
            method: 'POST',
            url: 'api/MstUniversitySection/MstUniversitySectionSuspend',
            data: $scope.newUS,
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
                    $scope.getUSList();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Add New University Section*/
    $scope.newUSAdd = function () {
        $state.go('MstUniversitySectionAdd');
    };

    /*Back to Edit Page of University Section*/
    $scope.backToList = function () {
        $state.go('MstUniversitySectionEdit');
    };
});
