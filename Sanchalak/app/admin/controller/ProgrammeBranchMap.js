app.controller('ProgrammeBranchMapCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage ProgrammeBranchMap";

    /*Reset ProgrammeBranchMap*/
    $scope.resetProgrammeBranchMap = function () {
        $scope.ProgrammeBranchMap = {};
    };

    /*Get ProgrammeBranchMap List*/
    $scope.getProgrammeBranchMap = function () {
        
        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/ProgrammeBranchMap/ProgrammeBranchMapGet',
            
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
                    $scope.ProgrammeBranchMapTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };    

    /*Get Faculty List*/   
    $scope.getFacultyList = function () {
        
        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGetForDropDown',
            //data: $scope.ProgrammeBranchMap,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.FacList = {};
                }
                else {
                    $scope.FacList = response.obj;
                }

            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Get Institute List*/
    $scope.getInstituteList = function () {

        $http({
            method: 'POST',
            url: 'api/MstInstitute/MstInstituteGetForDropDown',
            //data: $scope.FacultyInstituteMap,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.InstituteList = {};
                }
                else {
                    $scope.InstituteList = response.obj;
                }
            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Get Institute List By Type*/
    $scope.getInstituteListByType = function () {

        $http({
            method: 'POST',
            url: 'api/MstInstitute/MstInstituteGetByInstituteType',
            //data: $scope.FacultyInstituteMap,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.InstituteList = {};
                }
                else {
                    $scope.InstituteList = response.obj;
                }
            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Get Programme List By FacultyId*/
    $scope.getProgrammeList = function () {
        if ($scope.ProgrammeBranchMap.FacultyId != null && $scope.ProgrammeBranchMap.FacultyId != undefined) {
            var data = { FacultyId: $scope.ProgrammeBranchMap.FacultyId };
        } else {
            alert("Something Went Wrong!!!")
        }        
        
        $http({
            method: 'POST',
            url: 'api/MstProgrammePart/NewMstProgrammeGetByFacultyId',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.ProgList = {};
                }
                else {
                    $scope.ProgList = response.obj;
                }

            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Get Programme List By InstituteId*/
    $scope.getProgrammeListByInstituteId = function () {
        if ($scope.ProgrammeBranchMap.InstituteId != null && $scope.ProgrammeBranchMap.InstituteId != undefined) {
            var data = { InstituteId: $scope.ProgrammeBranchMap.InstituteId };
        } else {
            alert("Something Went Wrong!!!")
        }

        $http({
            method: 'POST',
            url: 'api/MstProgramme/MstProgrammeGetByInstituteId',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.ProgList = {};
                }
                else {
                    $scope.ProgList = response.obj;
                }

            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Get Programme List*/
    $scope.getProgrmLst = function () {
        $http({
            method: 'POST',
            url: 'api/MstProgramme/MstProgrammeListGet',
            data: $scope.ProgrammeBranchMap,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgList1 = response.obj;
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };      

    /*Get Branch List By FacultyId*/
    $scope.getBranchListByFacultyId = function () {
        if ($scope.ProgrammeBranchMap.FacultyId != null && $scope.ProgrammeBranchMap.FacultyId != undefined) {
            var data = { FacultyId: $scope.ProgrammeBranchMap.FacultyId };
        } else {
            alert("Something Went Wrong!!!")
        }  
        
        $http({
            method: 'POST',
            url: 'api/MstSpecialisation/MstSpecialisationGetByFacultyId',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.BranchList1 = {};
                }
                else {
                    $scope.BranchList1 = response.obj;
                }

            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Get Branch List By InstituteId*/
    $scope.getBranchListByInstituteId = function () {
        if ($scope.ProgrammeBranchMap.InstituteId != null && $scope.ProgrammeBranchMap.InstituteId != undefined) {
            var data = { InstituteId: $scope.ProgrammeBranchMap.InstituteId };
        } else {
            alert("Something Went Wrong!!!")
        }

        $http({
            method: 'POST',
            url: 'api/MstSpecialisation/MstSpecialisationGetByInstituteId',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.BranchList1 = {};
                }
                else {
                    $scope.BranchList1 = response.obj;
                }

            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Get Branch List By ProgrammeId*/
    $scope.getBranchListByProgrammeId = function () {
        
        $http({
            method: 'POST',
            url: 'api/MstProgramInstance/MstProgrammeBranchListGetByProgrammeId',
            data: $scope.ProgrammeBranchMap,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.BranchList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Get Specialisation List*/
    $scope.SpecialisationGet = function () {

        $http({
            method: 'POST',
            url: 'api/MstProgramInstance/MstProgrammeBranchListGet',
            data: $scope.ProgrammeBranchMap,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.BranchList1 = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Get Sub-Specialisation List*/
    $scope.MstSubSpecialisationGet = function () {
        $http({
            method: 'POST',
            url: 'api/ProgrammeBranchMap/SubSpecialisationGet',
            data: $scope.ProgrammeBranchMap,
            headers: { "Content-Type": 'application/json' }
        })

            .success(function (response) {
                $scope.SubList1 = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Get Sub-Specialisation By SpecialisationId*/
    $scope.getMstSubSpecialisationList = function () {
        
        console.log($scope.ProgrammeBranchMap);
        $http({
            method: 'POST',
            url: 'api/ProgrammeBranchMap/MstSubSpecialisationGetbyMstSpecialisationId',
            data: $scope.ProgrammeBranchMap,
            headers: { "Content-Type": 'application/json' }
        })

            .success(function (response) {
                $scope.SubList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Add ProgrammeBranchMap*/
    $scope.ProgrammeBranchMapAdd = function () {
         
        if ($scope.ProgrammeBranchMap.ProgrammeId === null || $scope.ProgrammeBranchMap.ProgrammeId === undefined ||
            $scope.ProgrammeBranchMap.SpecialisationId === null || $scope.ProgrammeBranchMap.SpecialisationId === undefined) { alert("Enter All Fields"); }
        else {

            $http({
                method: 'POST',
                url: 'api/ProgrammeBranchMap/ProgrammeBranchMapAdd',
                data: $scope.ProgrammeBranchMap,
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
                        $scope.ProgrammeBranchMap = {};
                        $scope.getProgrammeBranchMap();
                                                
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };       

    /*Modify ProgrammeBranchMap Data*/
    $scope.modifyProgrammeBranchMap = function (data) {
           
        $scope.showFormFlag = true;
        $scope.ProgrammeBranchMap = data;     
        $scope.getFacultyList();
        $scope.getProgrmLst();
        $scope.SpecialisationGet();
        $scope.MstSubSpecialisationGet();
        $(window).scrollTop(0); 
        };  

    /*Update ProgrammeBranchMap*/
    $scope.editProgrammeBranchMap = function () {
                
        $http({
            method: 'POST',
            url: 'api/ProgrammeBranchMap/ProgrammeBranchMapUpdate',
            data: $scope.ProgrammeBranchMap,
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
                    $scope.getProgrammeBranchMap();
                
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Delete ProgrammeBranchMap*/
    $scope.deleteProgrammeBranchMap = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.ProgrammeBranchMap = data;

            $http({
                method: 'POST',
                url: 'api/ProgrammeBranchMap/ProgrammeBranchMapDelete',
                data: $scope.ProgrammeBranchMap,
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
                        $scope.getProgrammeBranchMap();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
           
        }, function () {
            $scope.status = 'You decided to keep your debt.';
        });
    };       

    /*Add New ProgrammeBranchMap*/
    $scope.newProgrammeBranchMapAdd = function () {
        $state.go('ProgrammeBranchMapAdd');
    };

    /*Back to Edit Page of ProgrammeBranchMap*/
    $scope.backToList = function () {
        $state.go('ProgrammeBranchMapEdit');
    };

    /*Display ProgrammeBranchMap Data*/
    $scope.displayProgrammeBranchMap = function (data) {
        $scope.ProgrammeBranchMap = data;
    };

    /*Active Enable ProgrammeBranchMap*/
    $scope.ShowProgBranchMap = function (data) {

        $scope.newProgrammeBranchMap = data;

        $http({
            method: 'POST',
            url: 'api/ProgrammeBranchMap/ProgrammeBranchMapIsActiveEnable',
            data: $scope.newProgrammeBranchMap,
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
                    $scope.getProgrammeBranchMap();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Active Disable ProgrammeBranchMap*/
    $scope.HideProgBranchMap = function (data) {

        $scope.newProgrammeBranchMap = data;

        $http({
            method: 'POST',
            url: 'api/ProgrammeBranchMap/ProgrammeBranchMapIsActiveDisable',
            data: $scope.newProgrammeBranchMap,
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
                    $scope.getProgrammeBranchMap();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

});