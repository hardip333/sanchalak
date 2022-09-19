app.controller('IBMCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Institute Branch Mapping";

    var newSpelist = new Array();

    $scope.IBMTableParams = new NgTableParams({
    }, {
        dataset: $scope.IBMList
    });

    $scope.resetIBM = function () {
        $scope.IBM = {};
    };

    $scope.getIBMList = function () {

        var data = new Object();
        alert('hi');
        $http({
            method: 'POST',
            url: 'api/InstituteBranchMap/InstituteBranchMapGet',
            //data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');
                }
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.test = response.obj;
                    $scope.IBMTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                    console.log('====hi====');
                    console.log($scope.test);
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getFacultyList = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGet',
            data: data,
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
                    $scope.FacList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    }

    $scope.getProgrammeList = function () {

        
        if ($scope.IBM.FacultyId === null || $scope.IBM.FacultyId === undefined) {

            alert("please check all fields !!!");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/InstituteBranchMap/MstProgrammeListGet',
                data: $scope.IBM,
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
                        $scope.ProgList = response.obj;
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.getBranchListByProgrammeId = function () {
        if ($scope.IBM.ProgrammeId === null || $scope.IBM.ProgrammeId === undefined) {

            alert("please check all fields !!!");
        }
        else {
            $http({
                method: 'POST',
                url: 'api/InstituteBranchMap/MstProgrammeBranchListGetByProgrammeId',
                data: $scope.IBM,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    if (response.response_code == "0") {
                        $state.go('login');

                    }
                    else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        $scope.BranchList = response.obj;


                    }
                })
                .error(function (res) {
                    alert(res);
                });
        }
    };

    $scope.CheckChangeSpecialisation = function (Specialisation, checked) {
        if (checked) {
            newSpelist.push(Specialisation);
        }
        else {
           var SpeIndex = newSpelist.map(function (item) { return item.Id; }).indexOf(Specialisation.Id);
            newSpelist.splice(SpeIndex, 1);
        }
        $scope.newSpelist = newSpelist;
    };

    $scope.getInstituteList = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstInstitute/MstInstituteGet',
            data: $scope.IBM,
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
                    $scope.InstituteList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    }

    $scope.addIBM = function () {

        $scope.IBM.SpeList = $scope.newSpelist;
        if ($scope.IBM.InstituteId === null || $scope.IBM.InstituteId === undefined
            || $scope.IBM.ProgrammeId === null || $scope.IBM.ProgrammeId === undefined
            || $scope.IBM.SpeList === null || $scope.IBM.SpeList === undefined) {

            alert("please check all fields !!!");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/InstituteBranchMap/InstituteBranchMapAdd',
                data: $scope.IBM,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    if (response.response_code == "0") {
                        $state.go('login');
                    }
                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {

                        $scope.IBM = {};
                        $scope.BranchList = {};
                        $scope.getIBMList();
                        $mdDialog.show(
                            $mdDialog.alert()
                                .parent(angular.element(document.querySelector('#popupContainer')))
                                .clickOutsideToClose(true)
                                .title("Message")
                                .textContent(response.obj)
                                .ariaLabel('Alert Dialog Demo')
                                .ok('Okay!')
                        );
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });

        }
    };

    $scope.cancelIBM = function () {
        $scope.IBM = {};
        $scope.modifyIBMFlag = false;
    };

    $scope.modifyIBMData = function (data) {
        $scope.IBM = data;
        $scope.modifyIBMFlag = true;
        $scope.showFormFlag = true;
    };

    $scope.modifyIBM = function () {
        $http({
            method: 'POST',
            url: 'api/InstituteBranchMap/InstituteBranchMapEdit',
            data: $scope.IBM,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.IBM = {};
                    $scope.getIBMList();
                    $scope.modifyIBMFlag = false;
                    $scope.showFormFlag = false;
                    $mdDialog.show(
                        $mdDialog.alert()
                            .parent(angular.element(document.querySelector('#popupContainer')))
                            .clickOutsideToClose(true)
                            .title("Message")
                            .textContent(response.obj)
                            .ariaLabel('Alert Dialog Demo')
                            .ok('Okay!')
                    );
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.deleteIBM = function (ev, data) {

        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.IBM = data;
            $http({
                method: 'POST',
                url: 'api/InstituteBranchMap/InstituteBranchMapDelete',
                data: $scope.IBM,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    if (response.response_code == "0") {
                        $state.go('login');
                    }

                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        $scope.getIBMList();
                        $mdDialog.show(
                            $mdDialog.alert()
                                .parent(angular.element(document.querySelector('#popupContainer')))
                                .clickOutsideToClose(true)
                                .title("Message")
                                .textContent(response.obj)
                                .ariaLabel('Alert Dialog Demo')
                                .ok('Okay!')
                        );
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });

        }, function () {
            $scope.status = 'You decided to keep your debt.';
        });
    };

    $scope.showIBM = function (data) {

        $scope.newIBM = data;

        $http({
            method: 'POST',
            url: 'api/InstituteBranchMap/InstituteBranchMapIsActiveEnable',
            data: $scope.newIBM,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.getIBMList();


                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.hideIBM = function (data) {


        $scope.newIBM = data;

        $http({
            method: 'POST',
            url: 'api/InstituteBranchMap/InstituteBranchMapIsActiveDisable',
            data: $scope.newIBM,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.getIBMList();


                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.newIBMAdd = function () {
        $state.go('IBMAdd');
    };

    $scope.backToList = function () {
        $state.go('IBMEdit');
    };

    $scope.displayIBM = function (data) {
        $scope.IBM = data;
    };

});
