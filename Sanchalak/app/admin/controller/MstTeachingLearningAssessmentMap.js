app.controller('TeachingLearningAssessmentMapCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage MstTeachingLearningAssessmentMap";

    /*Reset TeachingLearningAssessmentMap*/
    $scope.resetMstTeachingLearningAssessmentMap = function () {
        $scope.MstTeachingLearningAssessmentMap = {};
    };

    /*Get TeachingLearningAssessmentMap List*/
    $scope.getTeachingLearningAssessmentMap = function () {
        
        var data = new Object();
        
        $http({
            method: 'POST',
            url: 'api/MstTeachingLearningAssessmentMap/MstTeachingLearningAssessmentMapGet',
           
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
                    $scope.MstTeachingLearningAssessmentMapTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };    

    /*Get TeachingLearning Method List*/
    $scope.getTeachingLearningMethod = function () {
        
        $http({
            method: 'POST',
            url: 'api/MstTeachingLearningMethod/MstTeachingLearningMethodGet',
            //data: $scope.MstTeachingLearningAssessmentMap,
            headers: { "Content-Type": 'application/json' }
        })

            .success(function (response) {
                $scope.TLMList = response.obj;

            })
            .error(function (res) {
               
            });
    };

    /*Get Assessment Method List*/
    $scope.getAssessmentMethod = function () {
        
        $http({
            method: 'POST',
            url: 'api/MstAssessmentMethod/MstAssessmentMethodGet',
            //data: $scope.MstTeachingLearningAssessmentMap,
            headers: { "Content-Type": 'application/json' }
        })

            .success(function (response) {
                $scope.AMList = response.obj;
               
            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Add TeachingLearningAssessmentMap*/
    $scope.addTeachingLearningAssessmentMap = function () {
        if ($scope.MstTeachingLearningAssessmentMap.TeachingLearningMethodId === null || $scope.MstTeachingLearningAssessmentMap.TeachingLearningMethodId === undefined ||
            $scope.MstTeachingLearningAssessmentMap.AssessmentMethodId === null || $scope.MstTeachingLearningAssessmentMap.AssessmentMethodId === undefined)
        { alert("Select All Fields"); }
        else {
            $http({
                method: 'POST',
                url: 'api/MstTeachingLearningAssessmentMap/MstTeachingLearningAssessmentMapAdd',
                data: $scope.MstTeachingLearningAssessmentMap,
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
                        $scope.MstTeachingLearningAssessmentMap = {};
                        $scope.getTeachingLearningAssessmentMap();
                       
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }

    };

    /*Modify TeachingLearningAssessmentMap Data*/
    $scope.modifyTeachingLearningAssessmentMapData = function (data) {
        
        $scope.showFormFlag = true;
        $scope.MstTeachingLearningAssessmentMap = data;
        if (!($scope.MstTeachingLearningMethodGet == null && $scope.MstTeachingLearningMethodGet == undefined)) { $scope.MstAssessmentMethodGet(); }
        
    };

    /*Update TeachingLearningAssessmentMap*/
    $scope.editTeachingLearningAssessmentMap = function () {
        
        $http({
            method: 'POST',
            url: 'api/MstTeachingLearningAssessmentMap/MstTeachingLearningAssessmentMapUpdate',
            data: $scope.MstTeachingLearningAssessmentMap,
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
                    $scope.showFormFlag = false;
                    $scope.getTeachingLearningAssessmentMap();

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Delete TeachingLearningAssessmentMap*/
    $scope.deleteTeachingLearningAssessmentMap = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.MstTeachingLearningAssessmentMap = data;

            $http({
                method: 'POST',
                url: 'api/MstTeachingLearningAssessmentMap/MstTeachingLearningAssessmentMapDelete',
                data: $scope.MstTeachingLearningAssessmentMap,
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
                        $scope.getTeachingLearningAssessmentMap();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
           
        }, function () {
            $scope.status = 'You decided to keep your debt.';
        });
    };    

    /*Add New TeachingLearningAssessmentMap*/
    $scope.newTeachingLearningAssessmentMapAdd = function () {
        $state.go('TeachingLearningAssessmentMapAdd');
    };

    /*Back to Edit Page of TeachingLearningAssessmentMap*/
    $scope.backToList = function () {
        $state.go('TeachingLearningAssessmentMapEdit');
    };

    /*Display TeachingLearningAssessmentMap Data*/
    $scope.displayTeachingLearningAssessmentMap = function (data) {
        $scope.MstTeachingLearningAssessmentMap = data;
    };

    /*Active Enable TeachingLearningAssessmentMap*/
    $scope.ShowTLMAMMap = function (data) {

        $scope.MstTeachingLearningAssessmentMap = data;

        $http({
            method: 'POST',
            url: 'api/MstTeachingLearningAssessmentMap/MstTeachingLearningAssessmentMapIsActive',
            data: $scope.MstTeachingLearningAssessmentMap,
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
                    $scope.getTeachingLearningAssessmentMap();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Active Disable TeachingLearningAssessmentMap*/
    $scope.HideTLMAMMap  = function (data) {

        $scope.MstTeachingLearningAssessmentMap = data;

        $http({
            method: 'POST',
            url: 'api/MstTeachingLearningAssessmentMap/MstTeachingLearningAssessmentMapIsSuspended',
            data: $scope.MstTeachingLearningAssessmentMap,
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
                    $scope.getTeachingLearningAssessmentMap();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

});