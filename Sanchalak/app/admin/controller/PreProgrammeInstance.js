app.controller('PreProgramInstanceCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {
    $rootScope.pageTitle = "Programme Instance";
    $scope.ProgInst = {};
    //$scope.ProgInstTableparam = new NgTableParams(
    //    {}, {
    //    dataset: $scope.ProgInstList
    //});
    // $scope.ProgInst = {};
    $scope.resetProgInst = function () {
        $scope.ProgInst = {};
    };

    $scope.setAdmissionRightValue = function () {
           
        if ($scope.ProgInst.AdmissionRight === undefined || $scope.ProgInst.AdmissionRight == null) {
            $scope.ProgInst.AdmissionRight = 'University';
        }
            
    };

    $scope.getProgrmLst = function () {
        $http({
            method: 'POST',
            url: 'api/MstProgramme/MstProgrammeListGet',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgIList = response.obj;
            })
            .error(function (res) {
                alert(res);
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

    $scope.getFacultyById = function () {
        console.log("Programme Instance:");
        console.log($scope.ProgInst);
        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGetbyId',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                //$scope.Faculty = response.obj[0]; // Archana madam's code
                $scope.Institute = response.obj[0];
               // $scope.Faculty = response.obj; // Krunal's code  
              
                $scope.getPreProgrammeList();
                $scope.getPreProgInstListByFacultyId();
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getPreProgrammeList = function () {
        
       // var facId = { FacultyId: $scope.Faculty.Id}
        var InstituteId = {InstituteId: $scope.Institute.InstituteId}
        $http({
            method: 'POST',
            url: 'api/MstProgramInstance/MstProgrammeListGet',
            data: InstituteId,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgList = response.obj;
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getProgrammeList = function () {
        $http({
            method: 'POST',
            url: 'api/MstProgramInstance/MstProgrammeListGet',
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

    $scope.getBranchListByProgrammeId = function () {

        $http({
            method: 'POST',
            url: 'api/MstProgramInstance/MstProgrammeBranchListGetByProgrammeId',
            data: $scope.ProgInst, //$localStorage.ProgrammeInstance
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.BranchList1 = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getBranchList = function () {

        $http({
            method: 'POST',
            url: 'api/MstProgramInstance/MstProgrammeBranchListGet',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.BranchList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    //$scope.getProgInstList = function () {
    //    var data = new Object();
    //    $http({
    //        method: 'POST',
    //        url: 'api/MstProgramInstance/IncProgramInstanceGet',
    //        data: data,
    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {
    //            if (response.response_code == "0") {
    //                $state.go('login');

    //            } else if (response.response_code != "200") {
    //                $rootScope.broadcast('dialog', "Error", "alert", response.obj);
    //            }
    //            else {
    //                $scope.ProgInstTableparam = new NgTableParams(
    //                    {}, {
    //                    dataset: response.obj
    //                });
    //            }
    //        })
    //        .error(function (res) {
    //            $rootScope.broadcast('dialog', "Error", "alert", res.obj);
    //        });
    //};
    //$scope.getProgInstList();

    $scope.getPreProgInstListByFacultyId = function () {
        
        var InstituteId = { InstituteId: $scope.Institute.InstituteId }
        
        $http({
            method: 'POST',
            url: 'api/MstProgramInstance/PreMstProgrammeInstanceListGetbyFacultyId',
            data: InstituteId,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                   // $scope.ProgInstListByFacultyId = response.obj;
                    $scope.ProgInstListByFacultyIdTableparam = new NgTableParams(
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

        if ($scope.ProgInst.InstituteId == null || $scope.ProgInst.InstituteId === undefined
            || $scope.ProgInst.ProgrammeId == null || $scope.ProgInst.ProgrammeId === undefined
            || $scope.ProgInst.SpecialisationId == null || $scope.ProgInst.SpecialisationId === undefined
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
            $scope.ProgInst.FacultyId = $scope.Institute.FacultyId;
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
                        alert(response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.ProgInst = {};
                        //$scope.getProgInstList();
                        $scope.getPreProgInstListByFacultyId();
                        // $scope.getDepartmentList();
                    }
                })
                .error(function (res) {
                    alert(res.obj);
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.preProgInstAdd = function () {
        
        if ($scope.ProgInst.ProgrammeId == null || $scope.ProgInst.ProgrammeId === undefined
            || $scope.ProgInst.AcademicYearId == null || $scope.ProgInst.AcademicYearId === undefined
            || $scope.ProgInst.AdmissionRight == null || $scope.ProgInst.AdmissionRight === undefined
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
            $scope.ProgInst.FacultyId = $scope.Institute.Id;
            $http({
                method: 'POST',
                url: 'api/MstProgramInstance/PreIncProgrammInstanceAdd',
                data: $scope.ProgInst,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        alert(response.obj);
                    }
                    else {
                        //alert(response.obj);
                        alert(response.obj.strMessage);
                        //alert(response.obj.ProgInstance);
                        //alert(response.obj.ProgrammeName);
                        if (response.obj.ProgInstance > 0) {
                            $localStorage.localObj = {};
                            $localStorage.localObj.ProgrammeInstance = response.obj.ProgInstance;
                            $localStorage.localObj.ProgrammeName = response.obj.ProgrammeName;
                            $localStorage.localObj.InstituteId = $scope.Institute.InstituteId;
                            /*--This is the flag for identify user move from Programme Instance to Programme Instance Part--*/
                            $localStorage.localObj.flagfromInstance = 1;

                            $localStorage.PreProgInstData = $scope.ProgInst;

                            $scope.ProgInst = {};
                            //$scope.getProgInstList();
                            $scope.getPreProgInstListByFacultyId();
                            $state.go('PreProgInstancePartConfigAdd');
                            // $scope.getDepartmentList();
                        }
                        else {
                            $scope.ProgInst = {};
                        }
                    }
                })
                .error(function (res) {
                    alert(res.obj);
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.modifyProgInstData = function (data) {
        $scope.showFormFlag = true;
        $scope.ProgInst = data;

    };

    $scope.modifyProgInst = function () {

        if ($scope.ProgInst.FacultyId == null || $scope.ProgInst.FacultyId === undefined
            || $scope.ProgInst.ProgrammeId == null || $scope.ProgInst.ProgrammeId === undefined
            || $scope.ProgInst.SpecialisationId == null || $scope.ProgInst.SpecialisationId === undefined
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
                url: 'api/MstProgramInstance/PreIncProgrammInstanceUpdate',
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
                        //$scope.getProgInstList();
                        $scope.getPreProgInstListByFacultyId();

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
                        //$scope.getProgInstList();
                        $scope.getPreProgInstListByFacultyId();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
            // };
        }, function () {
            $scope.status = 'You decided to keep your debt.';
            alert($scope.status);
        });
    };

    $scope.showProgInst = function (data) {
        // data.createdById = $rootScope.id;
        $scope.newProgInst = data;
        // $scope.InactiveFlag = true;
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
                    //$scope.getProgInstList();
                    $scope.getPreProgInstListByFacultyId();
                    // $scope.InactiveFlag = false;

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });


    };

    $scope.hideProgInst = function (data) {

        //  data.createdById = $rootScope.id;
        $scope.newProgInst = data;
        // $scope.InactiveFlag = false;
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
                    //$scope.getProgInstList();
                    $scope.getPreProgInstListByFacultyId();
                    //  $scope.InactiveFlag = true;

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.newProgInstAdd = function () {
        $state.go('ProgrammeInstanceAdd');
    };
    $scope.nextAdd = function () {
        $state.go('PreProgInstancePartConfigEdit');
    };

    $scope.backToList = function () {
        $state.go('PreProgInstanceConfigEdit');
    };

    $scope.displayProgInst = function (data) {
        $scope.ProgInst = data;
    };
    //PreProgramInstanceConfiguration
    $scope.newPreProgInstConfigAdd = function () {
        $state.go('PreProgInstanceConfigAdd');
    };


});
