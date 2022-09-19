app.controller('ResMarksLevelsCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Res Marks Levels";

    $scope.cardTitle = "Res Marks Levels Operation";

    $scope.getResMarksLevelList = function () {

        $http({
            method: 'POST',
            url: 'api/ResMarksLevels/ResMarksLevelsGet',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                else
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {

                        $scope.MarksLevelTableParams = new NgTableParams({
                        }, {
                            dataset: response.obj
                        });
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.addResMarksLevel = function () {
        debugger;
       
        if ($scope.LevelMarks.MarksLevelName == null || $scope.LevelMarks.MarksLevelName == undefined
        ) {
            $scope.modifyUserFlag = true;
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#EligibilityGroupform')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please Fill all Mandatory details before Add...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
            debugger;
            $http({
                method: 'POST',
                url: 'api/ResMarksLevels/ResMarksLevelsAdd',
                data: $scope.LevelMarks,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;
                    if (response.response_code == "0") {
                        $state.go('login');
                    }
                    else
                        if (response.response_code != "200") {
                            $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        }
                        else {
                            alert(response.obj);
                            console.log($scope.LevelMarks);
                            debugger;
                            $scope.LevelMarks = {};
                            $scope.getResMarksLevelList();
                        }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.modifyResMarksLevelData = function (data) {
        $scope.showFormFlag = true;
        $scope.Marks = data;
        $scope.getResMarksLevelList();
    };

    $scope.editResMarksLevel = function () {
     
        if ($scope.Marks.MarksLevelName == null || $scope.Marks.MarksLevelName === undefined         
        ) {
            $scope.modifyUserFlag = true;
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#EligibilityGroupform')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please Fill all Mandatory details before Update...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
            $http({
                method: 'POST',
                url: 'api/ResMarksLevels/ResMarksLevelsEdit',
                data: $scope.Marks,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    //  $rootScope.showLoading = false;
                    if (response.response_code == "0") {
                        $state.go('login');
                    }
                    else
                        if (response.response_code != "200") {
                            $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        }
                        else {
                            alert(response.obj);
                            $scope.Marks = {};
                            $scope.getResMarksLevelList();
                            $scope.showFormFlag = false;
                        }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.deleteResMarksLevel = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('Record will be deleted permanently.')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');
        $mdDialog.show(confirm).then(function () {

            $scope.Marks = data;
            $http({
                method: 'POST',
                url: 'api/ResMarksLevels/ResMarksLevelsDelete',
                data: $scope.Marks,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;
                    if (response.response_code == "0") {
                        $state.go('login');
                    }
                    else
                        if (response.response_code != "200") {
                            $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        }
                        else {
                            alert(response.obj);
                            $scope.Marks = {};
                            $scope.getResMarksLevelList();

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

    $scope.newResMarksLevelAdd = function () {
        $state.go('ResMarksLevelsAdd');
    };
    $scope.nextAdd = function () {
        $state.go('ResMarksLevelsEdit');
    };
    $scope.backToList = function () {
        $state.go('ResMarksLevelsEdit');
    };
    $scope.displayResMarksLevel = function (data) {
        $scope.Marks = data;
    };
    $scope.cancelResMarksLevel = function () {
        $scope.Marks = {};
        $scope.modifyUserFlag = false;
    }

    $scope.modifyResMarksLevel = function (data) {
        $scope.Marks = data;
        $scope.modifyUserFlag = true;
    }

});