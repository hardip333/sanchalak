app.controller('ProgrammeInstanceCtrl', function ($scope, $localStorage, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {
    $rootScope.pageTitle = "Programme Instance";
    $scope.ProgInstList = [];
    $scope.showDefineBtn = false;
    $scope.ProgInstTableparam = new NgTableParams(
        {}, {
        dataset: $scope.ProgInstList
    });

    $scope.resetProgInst = function () {
        localStorage.clear();
        $scope.ProgInst = {};
    };

    $scope.getProgrammeList = function () {
       
        $http({
            method: 'POST',
            url: 'api/MstProgramme/MstProgrammeGetByFacultyId',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgList = response.obj;
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };   

    $scope.getFacultyList = function () {

        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGet',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.FacList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getAcademicList = function () {
        
        $http({
            method: 'POST',
            url: 'api/MstProgramInstance/AcademicYearGet',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.AcadList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };    

    $scope.getProgInstList = function () {
        var data = new Object();
        $http({
            method: 'POST',
            url: 'api/MstProgramInstance/IncProgramInstanceGet',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.ProgInstTableparam = new NgTableParams(
                        {}, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.broadcast('dialog', "Error", "alert", res.obj);
            });
    };     
    
    $scope.progInstAdd = function () {

        if ($scope.ProgInst.FacultyId == null || $scope.ProgInst.FacultyId === undefined
            || $scope.ProgInst.ProgrammeId == null || $scope.ProgInst.ProgrammeId === undefined
            //|| $scope.ProgInst.SpecialisationId == null || $scope.ProgInst.SpecialisationId === undefined
            || $scope.ProgInst.AcademicYearId == null || $scope.ProgInst.AcademicYearId === undefined
            || $scope.ProgInst.Intake == null || $scope.ProgInst.Intake === undefined) {

            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please complete the form before Click...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstProgramInstance/IncProgrammInstanceAdd',
                data: $scope.ProgInst,
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

                        $scope.ProgInst.Id = response.obj.Item2;
                        //alert(response.obj);  
                        //alert(response.obj.Item2);
                        //$scope.ProgInst = {};
                        $scope.getProgInstList();
                        alert(response.obj.Item1); 
                        if (response.obj.Item1 != "Record already exists.") {
                            $scope.showDefineBtn = true;
                        }
                        /*else {
                            $scope.showDefineBtn = false;
                        }*/
                        
                    }
                })
                .error(function (res) {
                    alert(res.obj);
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.defineProgInstPart = function () {
        $localStorage.define1 = {};
        $localStorage.define1.FacId = $scope.ProgInst.FacultyId;
        $localStorage.define1.ProgId = $scope.ProgInst.ProgrammeId;
        $localStorage.define1.ProgInstId = $scope.ProgInst.Id;
        $localStorage.define1.AcadId = $scope.ProgInst.AcademicYearId;
        $state.go('ProgrammeInstancePartAdd');
        
    };

    $scope.modifyProgInstData = function (data) {
        $scope.showFormFlag = true;
        $scope.ProgInst = data;
        if (!($scope.getFacultyList == null && $scope.getFacultyList == undefined)) { $scope.getProgrammeList(); }
       // if (!($scope.getProgrammeList == null && $scope.getProgrammeList == undefined)) { $scope.getBranchListByProgrammeId(); }
        $(window).scrollTop(0);
    };

    $scope.modifyProgInst = function () {

        if ($scope.ProgInst.FacultyId == null || $scope.ProgInst.FacultyId === undefined
            || $scope.ProgInst.ProgrammeId == null || $scope.ProgInst.ProgrammeId === undefined
           // || $scope.ProgInst.SpecialisationId == null || $scope.ProgInst.SpecialisationId === undefined
            || $scope.ProgInst.AcademicYearId == null || $scope.ProgInst.AcademicYearId === undefined
            || $scope.ProgInst.Intake == null || $scope.ProgInst.Intake === undefined) {

            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please complete the form before Click...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstProgramInstance/IncProgrammInstanceUpdate',
                data: $scope.ProgInst,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        alert(response.obj);
                        $rootScope.broadcast('dialog', "Error", "alert", response.obj);
                    } else {
                        alert(response.obj);
                        $scope.ProgInst = {};
                        $scope.showFormFlag = false;
                        $scope.getProgInstList();

                    }
                })
                .error(function (res) {
                    alert(res.obj);
                    $rootScope.broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.deleteProgInst = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.ProgInst = data;

            $http({
                method: 'POST',
                url: 'api/MstProgramInstance/IncProgrammInstanceDelete',
                data: $scope.ProgInst,
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
                        $scope.getProgInstList();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
           
        }, function () {
                $scope.status = 'You decided to keep your debt.';
                alert($scope.status);
        });
    };      

    $scope.showProgInst = function (data) {
        
        $scope.newProgInst = data;
        
        $http({
            method: 'POST',
            url: 'api/MstProgramInstance/MstProgrammeInstIsActiveEnable',
            data: $scope.newProgInst,
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
                    $scope.getProgInstList();
                    
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });


    };

    $scope.hideProgInst = function (data) {

        $scope.newProgInst = data;
        
        $http({
            method: 'POST',
            url: 'api/MstProgramInstance/MstProgrammeInstIsActiveDisable',
            data: $scope.newProgInst,
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
                    $scope.getProgInstList();
                    
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.newProgInstAdd = function () {
        $state.go('ProgrammeInstanceAdd');
    };

    $scope.backToList = function () {
        localStorage.clear();
        $state.go('ProgrammeInstanceEdit');
    };

    $scope.displayProgInst = function (data) {
        $scope.ProgInst = data;
    };
});
