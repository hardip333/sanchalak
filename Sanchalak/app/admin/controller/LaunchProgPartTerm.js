app.controller('LaunchProgPartTermCtrl', function ($scope, $localStorage, $http, $rootScope, $state, $cookies, $mdDialog,  NgTableParams) {

    $rootScope.pageTitle = "Manage LaunchProgPartTermCtrl";

    $scope.LaunchProgInstPartTerm = {};

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
    $scope.getLaunchPartTermByFacIdAndAcadId = function () {

        if ($scope.LaunchProgInstPartTerm.FacultyId == null || $scope.LaunchProgInstPartTerm.FacultyId === undefined
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
        else if ($scope.LaunchProgInstPartTerm.AcademicYearId == null || $scope.LaunchProgInstPartTerm.AcademicYearId === undefined
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

            /*$localStorage.data1 = {};
            $localStorage.data1.FacultyId = $scope.LaunchProgInstPartTerm.FacultyId;
            $localStorage.data1.AcademicYearId = $scope.LaunchProgInstPartTerm.AcademicYearId;*/
            
            var data = {
                FacultyId: $scope.LaunchProgInstPartTerm.FacultyId,
                AcademicYearId: $scope.LaunchProgInstPartTerm.AcademicYearId
            };
            /*var data = {
                FacultyId: $localStorage.data1.FacultyId,
                AcademicYearId: $localStorage.data1.AcademicYearId
            };*/

            $http({
                method: 'POST',
                url: 'api/LaunchProgrammePartTerm/ProgrammeInstancePartTermGetByFacIdAndAcadId',
                data: data,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;
                    if (response.response_code == "0") {
                        $go.state('login');
                    }
                    else
                        if (response.response_code != "200") {
                            $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        }
                        else {

                            $scope.ProgInstPartTermTableParams1 = new NgTableParams({
                            }, {
                                dataset: response.obj
                            });

                        }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Get All Programme Instance Part Term's Data*/
    $scope.ProgrammeInstPartTermMapForLaunch = function () {
      
        $http({
            method: 'POST',
            url: 'api/LaunchProgrammePartTerm/ProgrammeInstancePartTermGet',
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
                    $scope.ProgInstPartTermTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                    console.log(response.obj[0].Islaunch);                  
                   
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };      

    /*Reset Selection*/
    $scope.resetSelection = function () {
        $scope.LaunchProgInstPartTerm = {};
    };

    /*Display Academic Year Data*/
    $scope.displayFullData = function (data) {
        
        $scope.newLaunchProgInstPartTerm = data;
        
    };

    /*Launch Programme Instance Part Term*/
    $scope.LaunchProgPartTerm = function (data) {
        
        $scope.newLaunchProgInstPartTerm = data;
        $http({
            method: 'POST',
            url: 'api/LaunchProgrammePartTerm/ProgrammeInstancePartTermLaunch',
            data: $scope.newLaunchProgInstPartTerm,
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
                    
                    $scope.getLaunchPartTermByFacIdAndAcadId();
                  
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    /*Un-Launch Programme Instance Part Term*/
    $scope.UnLaunchProgPartTerm = function (data) {
        $scope.newLaunchProgInstPartTerm = data;
        $http({
            method: 'POST',
            url: 'api/LaunchProgrammePartTerm/ProgrammeInstancePartTermUnLaunch',
            data: $scope.newLaunchProgInstPartTerm,
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

                    $scope.getLaunchPartTermByFacIdAndAcadId();

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    /*Launch Assessment Report*/
    $scope.AssessmentReportLaunch = function (data) {

        $scope.newLaunchProgInstPartTerm = data;
        $http({
            method: 'POST',
            url: 'api/LaunchProgrammePartTerm/AssessmentReportLaunch',
            data: $scope.newLaunchProgInstPartTerm,
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

                    $scope.getLaunchPartTermByFacIdAndAcadId();

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    /*Un-Launch Assessment Report*/
    $scope.AssessmentReportUnLaunch = function (data) {
        $scope.newLaunchProgInstPartTerm = data;
        $http({
            method: 'POST',
            url: 'api/LaunchProgrammePartTerm/AssessmentReportUnLaunch',
            data: $scope.newLaunchProgInstPartTerm,
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

                    $scope.getLaunchPartTermByFacIdAndAcadId();

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

});
