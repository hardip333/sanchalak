app.controller('MstProgInstPartTermPaperMapCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    /*Variable Declration */
    $scope.ProgInst = {};
    $scope.ProgInstPartTerm = {};
    $scope.IncProgInstPartTermPaperMap = {};
    $scope.paper = {};
    $scope.NoRecLabel = false;
    $scope.NoRecLabel1 = false;
    $scope.PaperMapDisable = true;

    $scope.resetPaperMap=function(){
        $scope.IncProgInstPartTermPaperMap = {};
        $scope.NoRecLabel = false;
        $scope.PaperDataTable = false;
        PaperAttachedTable.visible = false;
        NoRecLbl.visible = false;

        $scope.NoRecLabel1 = false;
        $scope.PaperDataTable1 = false;
        PaperAttachedTable1.visible = false;
        NoRecLbl1.visible = false;
        $scope.getFacultyList();
        $scope.getProgrmLst();
        $scope.getProgrmPartLst();
        $scope.getProgrmPartTermLst();
       
    };

    $scope.getFacultyList = function () {
        $scope.FacList = {};
        $http({
            method: 'POST',
            url: 'api/MstProgrammePartTermPaperMap/MstFacultyGetForDropDown',
            //data: $scope.Faculty,
            headers: { "Content-Type": 'application/json' }
        })
           

            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    if (response.response_code == "201") {
                        $scope.FacList = {};

                    }
                }
                else {
                    $scope.FacList = response.obj;
                    $scope.getProgrmLst();

                }
            })


            .error(function (res) {
                alert(res);
            });
    };

  


    $scope.getProgrmLst = function () {
        $scope.ProgIList = {};
        $http({
            method: 'POST',
            url: 'api/MstProgrammePartTermPaperMap/ProgrammeListGetByFacultyId',
            data: $scope.IncProgInstPartTermPaperMap,
            headers: { "Content-Type": 'application/json' }
        })
           
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    if (response.response_code == "201") {
                        $scope.ProgIList = {};

                    }
                }
                else {
                    $scope.ProgIList = response.obj;
                    $scope.PaperMapDisable = false

                }
            })


            .error(function (res) {
                alert(res);
            });
    };

    $scope.getProgrmPartLst = function () {
        $scope.ProgPartList = {};
        $http({
            method: 'POST',
            url: 'api/MstProgrammePartTermPaperMap/ProgrammePartGetByProgrammeId',
            data: $scope.IncProgInstPartTermPaperMap,
            headers: { "Content-Type": 'application/json' }
        })
           

            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    if (response.response_code == "201") {
                        $scope.ProgPartList = {};

                    }
                }
                else {
                    $scope.ProgPartList = response.obj;
                    $scope.PaperMapDisable = false;

                }
            })
            .error(function (res) {
                alert(res);
            });
    };


    $scope.getProgrmPartTermLst = function () {
        $scope.ProgPartTermList = {};
        $http({
            method: 'POST',
            url: 'api/MstProgrammePartTermPaperMap/ProgrammePartTermGetByPartId',
            data: $scope.IncProgInstPartTermPaperMap,
            headers: { "Content-Type": 'application/json' }
        })
           

            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    if (response.response_code == "201") {
                        $scope.ProgPartTermList = {};

                    }
                }
                else {
                    $scope.ProgPartTermList = response.obj;
                    $scope.PaperMapDisable = false;

                }
            })
            .error(function (res) {
                alert(res);
            });
    };

    /*It will call the methods having list of papers attached to the part term and not attached */
    $scope.ShowList = function () {

        if (
            $scope.IncProgInstPartTermPaperMap.FacultyId === null || $scope.IncProgInstPartTermPaperMap.FacultyId === undefined ||
            $scope.IncProgInstPartTermPaperMap.ProgrammeId === null || $scope.IncProgInstPartTermPaperMap.ProgrammeId === undefined ||
            $scope.IncProgInstPartTermPaperMap.PartTermId === null || $scope.IncProgInstPartTermPaperMap.PartTermId === undefined ||
           $scope.IncProgInstPartTermPaperMap.PartId === null || $scope.IncProgInstPartTermPaperMap.PartId === undefined
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
            url: 'api/MstProgrammePartTermPaperMap/AttachedPaperList',
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
                       
                        $scope.NoRecLabel = true;
                        $scope.PaperDataTable = false;
                        PaperAttachedTable.visible = false;
                        NoRecLbl.visible = true;
                    }
                    else {
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
            url: 'api/MstProgrammePartTermPaperMap/NotAttachedPaperList',
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
                      
                        $scope.NoRecLabel1 = true;
                        $scope.PaperDataTable1 = false;
                        PaperAttachedTable1.visible = false;
                        NoRecLbl1.visible = true;
                    }
                } else if (response.response_code == "200") {
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
        $http({
            method: 'POST',
            url: 'api/MstProgrammePartTermPaperMap/MstProgInstPartTermPaperMapAdd',
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
        $http({
            method: 'POST',
            url: 'api/MstProgrammePartTermPaperMap/MstProgInstPartTermPaperMapDelete',
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