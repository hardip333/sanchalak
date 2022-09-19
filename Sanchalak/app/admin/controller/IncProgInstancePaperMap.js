app.controller('IncProgInstPartTermPaperMapCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    /*Variable Declration */
    $scope.ProgInst = {};
    $scope.ProgInstPartTerm = {};
    $scope.IncProgInstPartTermPaperMap = {};
    $scope.paper = {};
    $scope.NoRecLabel = false;
    $scope.NoRecLabel1 = false;

    /*Academic Year List Get Method */
    $scope.IncAcademicYearListGet = function () {
        
        $http({
            method: 'POST',
            url: 'api/IncAcademicYear/AcademicYearGet',            
            headers: { "Content-Type": 'application/json' }
        })

            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.AcademicList = {};
                }
                else {
                    $scope.AcademicList = response.obj;
                }
            })
            .error(function (res) {
                //alert(res);
            });
    };

    /* Faculty List Get Method*/
    $scope.FacultyGet = function () {
       
        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGet',           
            headers: { "Content-Type": 'application/json' }
        })

            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.FacultyList = {};
                }
                else {
                    $scope.FacultyList = response.obj;
                }

            })
            .error(function (res) {
                //alert(res);
            });
    };

    /*Programme Instance List By Academic Year Id and Faculty Id */
    $scope.getProgrammeInstanceListByAcadId = function () {
        $scope.ProgInst.FacultyId = $scope.IncProgInstPartTermPaperMap.FacultyId;
        $scope.ProgInst.AcademicYearId = $scope.IncProgInstPartTermPaperMap.AcademicYearId;
        $http({
            method: 'POST',
            url: 'api/MstProgramInstance/InstanceListGetbyFacultyIdAndAcadId',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.InstList = {};
                }
                else {
                    $scope.InstList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Branch/Specialisation List By Programme Instance Id */
    $scope.getBranchListByProgInstId = function () {
        $scope.ProgInst.Id = $scope.IncProgInstPartTermPaperMap.ProgrammeInstanceId;
        
        $http({
            method: 'POST',
            url: 'api/IncProgInstPartTermPaperMap/MstProgrammeBranchListGetByProgInstanceId',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })

            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.BranchList = {};
                }
                else {
                    $scope.BranchList = response.obj;
                }

            })
            .error(function (res) {
                //alert(res);
            });
    };

    /*Programme Part List By Programme Instance Id */
    $scope.getProgrammePartListByProgInstId = function () {

        $scope.ProgInst.Id = $scope.IncProgInstPartTermPaperMap.ProgrammeInstanceId;
        $http({
            method: 'POST',
            url: 'api/IncProgInstPartTermPaperMap/ProgrammePartGetByProgInstId',
            data: $scope.IncProgInstPartTermPaperMap,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.ProgPartList = {};
                }
                else {
                    $scope.ProgPartList = response.obj;
                }

            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Programme Part Term List By Programme Instance Id */
    $scope.getProgPartTermListByProgInstPartId = function () {

        //$scope.ProgInstPartTerm.ProgrammeInstanceId = $scope.IncProgInstPartTermPaperMap.ProgrammeInstanceId;
        $scope.ProgInstPartTerm.ProgrammeInstancePartId = $scope.IncProgInstPartTermPaperMap.ProgrammePartId;
        $scope.ProgInstPartTerm.SpecialisationId = $scope.IncProgInstPartTermPaperMap.SpecialisationId;
        $http({
            method: 'POST',
            url: 'api/IncProgInstPartTermPaperMap/ProgrammePartTermGetByProgInstId',
            data: $scope.ProgInstPartTerm,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.ProgPartTermList = {};
                }
                else {
                    $scope.ProgPartTermList = response.obj;
                }

            })
            .error(function (res) {
                alert(res);
            });
    };

    /*It will call the methods having list of papers attached to the part term and not attached */
    $scope.ShowList = function () {

        if ($scope.IncProgInstPartTermPaperMap.AcademicYearId === null || $scope.IncProgInstPartTermPaperMap.AcademicYearId === undefined ||
            $scope.IncProgInstPartTermPaperMap.FacultyId === null || $scope.IncProgInstPartTermPaperMap.FacultyId === undefined ||
            $scope.IncProgInstPartTermPaperMap.ProgrammeInstanceId === null || $scope.IncProgInstPartTermPaperMap.ProgrammeInstanceId === undefined ||
            $scope.IncProgInstPartTermPaperMap.ProgrammePartId === null || $scope.IncProgInstPartTermPaperMap.ProgrammePartId === undefined ||
            $scope.IncProgInstPartTermPaperMap.SpecialisationId === null || $scope.IncProgInstPartTermPaperMap.SpecialisationId === undefined ||
            $scope.IncProgInstPartTermPaperMap.ProgrammeInstancePartTermId === null || $scope.IncProgInstPartTermPaperMap.ProgrammeInstancePartTermId === undefined
        ) {

            alert("please select all fields !!!");
        }
        else {
            $scope.AttachedPaperList();
            $scope.NotAttachedPaperList();
        }

    };

    /* List of papers Attached to the Programme Instance Part Term */
    $scope.AttachedPaperList = function () {
        
        $http({
            method: 'POST',
            url: 'api/IncProgInstPartTermPaperMap/AttachedPaperList',
            data: $scope.IncProgInstPartTermPaperMap,
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

                    if (response.obj === "No Record Found") {                        
                        console.log(response.obj);
                        $scope.NoRecLabel = true;
                        console.log($scope.NoRecLabel);
                        $scope.PaperDataTable = false;
                        PaperAttachedTable.visible = false;
                        NoRecLbl.visible = true;
                    } else {
                        $scope.NoRecLabel = false;
                        $scope.PaperDataTable = true;
                        PaperAttachedTable.visible = true;
                        $scope.AttachedPaperTable = new NgTableParams({
                        }, {
                            dataset: response.obj
                        });
                    }
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /* List of Papers Not attached to the Programme Instance Part Term */
    $scope.NotAttachedPaperList = function () {
        
        $http({
            method: 'POST',
            url: 'api/IncProgInstPartTermPaperMap/NotAttachedPaperList',
            data: $scope.IncProgInstPartTermPaperMap,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);



                    if (response.obj == "No Record Found") {
                        console.log("******************2");
                        $scope.NoRecLabel1 = true;
                        console.log($scope.NoRecLabel1);
                        $scope.PaperDataTable1 = false;
                        PaperAttachedTable1.visible = false;
                        NoRecLbl1.visible = true;
                    }
                } else if (response.response_code == "200") {
                            console.log(response.obj);
                            $scope.NoRecLabel1 = false;
                            $scope.PaperDataTable1 = true;
                            PaperAttachedTable1.visible = true;
                            $scope.AllPaperLstTable = new NgTableParams({
                            }, {
                                dataset: response.obj
                            });
                        }
                    
                
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /* To add the entry of the given paper from ProgrammeInstancePartTermPaperMap Table */
    $scope.AttachToLst = function (paper) {
        $scope.IncProgInstPartTermPaperMap.PaperId = paper.Id;
        console.log("PaperMap Model:");
        console.log($scope.IncProgInstPartTermPaperMap);
        $http({
            method: 'POST',
            url: 'api/IncProgInstPartTermPaperMap/IncProgInstPartTermPaperMapAdd',
            data: $scope.IncProgInstPartTermPaperMap,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    //Redirect user to login page
                    $state.go('login');
                    //return false;
                } else if (response.response_code != "200") {
                    // alert(response.obj);
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else if (response.response_code == "200") {
                
                
                        $scope.ShowList();
                        alert(response.obj);
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /* To delete the entry of the given paper from ProgrammeInstancePartTermPaperMap Table */
    $scope.DetachFrmLst = function (paper) {
        $scope.IncProgInstPartTermPaperMap.Id = paper.Id;
        //console.log("PaperMap Model:");
        //console.log($scope.IncProgInstPartTermPaperMap);
        $http({
            method: 'POST',
            url: 'api/IncProgInstPartTermPaperMap/IncProgInstPartTermPaperMapDelete',
            data: $scope.IncProgInstPartTermPaperMap,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    //Redirect user to login page
                    $state.go('login');
                    //return false;
                } else if (response.response_code != "200") {
                    // alert(response.obj);
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.ShowList();
                    alert(response.obj);
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /* Paper Detail for Modal */
    $scope.displayPaper = function (data) {
        $scope.paper = data;
    };

});