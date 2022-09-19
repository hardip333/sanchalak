app.controller('InstancePreferenceCountCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage InstancePreferenceCount";

    /*Reset InstancePreferenceCount*/
    $scope.resetInstancePreferenceCount = function () {
        $scope.InstancePreferenceCount = {};
    };

    /*Get InstancePreferenceCount List*/
    $scope.getInstancePreferenceCount = function (FacultyId) {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/InstancePreferenceCount/InstancePreferenceCountGet',
            data: { FacultyId: $localStorage.facultyDepartIntituteId },
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
                    $scope.InstancePreferenceCountTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };


    $scope.getFacultyById = function (FacultyName) {
        console.log("Programme Instance:");
        console.log($scope.InstancePreferenceCount);
        $http({
            method: 'POST',
            url: 'api/InstancePreferenceCount/MstFacultyGetbyId',
            data: { FacultyName: $localStorage.facultyName },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {


                //$scope.Faculty = response.obj[0]; // Archana madam's code
                $scope.Institute = response.obj[0];
                // $scope.Faculty = response.obj; // Krunal's code  

                // $scope.getPreProgrammeList();
                $scope.getIncProgrammeInstancePartTerm();
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getIncProgrammeInstancePartTerm = function () {
        //alert($scope.Institute.FacultyId);
        var FacultyId = { Id: $scope.Institute.Id }
        //alert(FacultyId);
        $http({
            method: 'POST',
            url: 'api/InstancePreferenceCount/ProgPartTermGetByFacultyId',
            data: FacultyId,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgInstPartTermList = response.obj;
                console.log(response.obj);
            })
            .error(function (res) {
                alert(res);
            });
    };


    /*Add InstancePreferenceCount*/
    $scope.addInstancePreferenceCount = function () {

        $http({
            method: 'POST',
            url: 'api/InstancePreferenceCount/InstancePreferenceCountAdd',
            data: $scope.InstancePreferenceCount,
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
                    $scope.InstancePreferenceCount = {};
                    $scope.getInstancePreferenceCount();
                    $state.go('InstancePreferenceCountEdit');

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    /*Modify InstancePreferenceCount Data*/
    $scope.modifyInstancePreferenceCountData = function (data) {
        $scope.InstancePreferenceCount = data;
        $scope.getFacultyById();
        $scope.getIncProgrammeInstancePartTerm();
        $scope.showFormFlag = true;
        $(window).scrollTop(0);
    };

    /*Update InstancePreferenceCount*/
    $scope.editInstancePreferenceCount = function () {


        $http({
            method: 'POST',
            url: 'api/InstancePreferenceCount/InstancePreferenceCountUpdate',
            data: $scope.InstancePreferenceCount,
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
                    $scope.getInstancePreferenceCount();
                    $scope.showFormFlag = false;

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    /*Delete InstancePreferenceCount*/
    $scope.deleteInstancePreferenceCount = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.InstancePreferenceCount = data;

            $http({
                method: 'POST',
                url: 'api/InstancePreferenceCount/InstancePreferenceCountDelete',
                data: $scope.InstancePreferenceCount,
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
                        $scope.getInstancePreferenceCount();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
            // };
        }, function () {
            $scope.status = 'You decided to keep your debt.';
        });
    };

    /*Add New InstancePreferenceCount*/
    $scope.newInstancePreferenceCountAdd = function () {
        $state.go('InstancePreferenceCountAdd');
    };

    /*Back to Edit Page of InstancePreferenceCount*/
    $scope.backToList = function () {
        $state.go('InstancePreferenceCountEdit');
    };

    /*Display InstancePreferenceCount Data*/
    $scope.displayInstancePreferenceCount = function (data) {
        $scope.InstancePreferenceCount = data;
    };

    ///*Active Enable InstancePreferenceCount*/
    //$scope.ShowInstancePreferenceCount = function (data) {

    //    $scope.Evaluation = data;

    //    $http({
    //        method: 'POST',
    //        url: 'api/MstEvaluation/EvaluationIsActive',
    //        data: $scope.Evaluation,
    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {
    //            if (response.response_code == "0") {
    //                $state.go('login');

    //            } else if (response.response_code != "200") {
    //                $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
    //            }
    //            else {
    //                alert(response.obj); 
    //                $scope.getEvaluation();
    //            }
    //        })
    //        .error(function (res) {
    //            $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
    //        });
    //};

    ///*Active Disable InstancePreferenceCount*/
    //$scope.HideEvaluation = function (data) {

    //    $scope.Evaluation = data;

    //    $http({
    //        method: 'POST',
    //        url: 'api/MstEvaluation/EvaluationIsSuspended',
    //        data: $scope.Evaluation,
    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {
    //            if (response.response_code == "0") {
    //                $state.go('login');

    //            } else if (response.response_code != "200") {
    //                $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
    //            }
    //            else {
    //                alert(response.obj); 
    //                $scope.getEvaluation();
    //            }
    //        })
    //        .error(function (res) {
    //            $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
    //        });
    //};

});