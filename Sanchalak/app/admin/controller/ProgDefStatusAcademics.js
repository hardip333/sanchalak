app.controller('ProgDefStatusAcademicsCtrl', function ($scope, $http, $filter, $localStorage, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }

    $scope.showData = false;
    $scope.ProgDefStatusAcademics = {}

    
    $scope.getFacultyList = function () {

        $http({
            method: 'POST',
            url: 'api/ProgDefStatusAcademics/FacultyGet',
            data: $scope.ProgDefStatusAcademics,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.FacList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getAcademicYear = function () {

        $http({
            method: 'POST',
            url: 'api/ProgDefStatusAcademics/AcademicYearGetForDropDown',
            data: $scope.ProgDefStatusAcademics,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.AcadList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getProgDefStatusAcademics = function () {

        //var data = new Object();

        $http({
            method: 'POST',
            url: 'api/ProgDefStatusAcademics/ProgDefStatusAcademicsGet',
            data: { FacultyId: $scope.ProgDefStatusAcademics.FacultyId, AcademicYearId: $scope.ProgDefStatusAcademics.AcademicYearId },
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
                    $scope.ProgDefStatusAcademicsTableParams = new NgTableParams({
                    }, {
                            dataset: response.obj

                    });
                    console.log(response.obj);
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Active Enable ProgDefStatusAcademics*/
    $scope.ShowProgDefStatusAcademics = function (data) {

        $scope.ProgDefStatusAcademics = data;

        $http({
            method: 'POST',
            url: 'api/ProgDefStatusAcademics/ProgDefStatusAcademicsIsActive',
            data: $scope.ProgDefStatusAcademics,
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
                    $scope.getProgDefStatusAcademics();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Active Disable ProgDefStatusAcademics*/
    $scope.HideProgDefStatusAcademics = function (data) {

        $scope.ProgDefStatusAcademics = data;

        $http({
            method: 'POST',
            url: 'api/ProgDefStatusAcademics/ProgDefStatusAcademicsIsSuspended',
            data: $scope.ProgDefStatusAcademics,
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
                    $scope.getProgDefStatusAcademics();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    
    


});

