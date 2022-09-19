app.controller('LaunchResultConfigurationCtrl', function ($scope, $localStorage, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage LaunchProgPartTermCtrl";

    $scope.LaunchResConfig = {};

    /*Faculty Data For DropDown*/
    $scope.getFacultyList = function () {

        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGet',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.FacList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Academic Year Data For DropDown*/
    $scope.getAcademicList = function () {

        $http({
            method: 'POST',
            url: 'api/MstProgramInstance/AcademicYearGet',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.AcadList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Get Programme Instance Part Term's Data By Faculty And Academic Year*/
    $scope.getInstancePartTermByFacIdAndAcadIdResConfig = function () {
        debugger;
        if ($scope.LaunchResConfig.FacultyId == null || $scope.LaunchResConfig.FacultyId === undefined
        ) {

            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#EligibilityGroupform')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please Select Faculty before Submit...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else if ($scope.LaunchResConfig.AcademicYearId == null || $scope.LaunchResConfig.AcademicYearId === undefined
        ) {

            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#EligibilityGroupform')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please Select Academic Year before Submit...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
            debugger;
            $http({
                method: 'POST',
                url: 'api/LaunchResultConfiguration/ResultConfigurationLaunchGet',
                data: $scope.LaunchResConfig,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;
                    if (response.response_code == "0") {
                       /* $go.state('login');*/
                    }
                    else
                        if (response.response_code != "200") {
                            $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        }
                        else {

                            $scope.ResProgInstPartTermTableParams= new NgTableParams({
                            }, {
                                dataset: response.obj
                            });
                            console.log(response.obj);
                            $scope.LaunchResConfig = response.obj;
                            console.log(response.obj);
                            for (var i = 0; i < $scope.LaunchResConfig.length; i++) {
                                if ($scope.LaunchResConfig[i].ResCalculationSts == true &&
                                    $scope.LaunchResConfig[i].ResCourseEvalSystemSts == true &&
                                    $scope.LaunchResConfig[i].ResCourseEvalSystemSts == true) {

                                    $scope.disableResultConig = true;
                                }

                            }
                            

                        }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.LaunchResConfiguration = function (data) {

        $scope.newLaunchProgResConfig = data;
        $http({
            method: 'POST',
            url: 'api/LaunchResultConfiguration/ResultConfigurationLaunch',
            data: $scope.newLaunchProgResConfig,
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

                    /*$scope.getLaunchPartTermByFacIdAndAcadId();*/

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };


    

    
});
