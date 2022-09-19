app.controller('MstInstituteProgrammeMapCtrl', function ($scope, $localStorage, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $scope.NoRecordFound = false;
    var ProgCount = 0;
    $scope.showSaveBtn = false;
    $rootScope.pageTitle = "Manage MstInstituteProgrammeMap";
    //$scope.checked = {};
    /*Reset Evaluation*/
    $scope.resetMstInstituteProgrammeMap = function () {
        $scope.MstInstituteProgrammeMap = {};
    };

    $scope.getMstInstituteProgrammeMap = {};
    $scope.CheckChangeProg = {};

    /*Get Evaluation List*/
    $scope.getMstInstituteProgrammeMap = function (InstituteId) {

        $scope.InstituteId = $localStorage.InstituteId;
        //alert($scope.InstituteId );
        //alert("Local :"+$localStorage.InstituteId);
        $scope.InstituteName = $localStorage.InstituteName;
        $http({
            method: 'POST',
            url: 'api/MstInstituteProgrammeMap/MstInstituteProgrammeMapGet',
            data: { InstituteId: $localStorage.InstituteId },
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
                    console.log(response.obj);
                    if (response.obj === "Record Not Found") {

                        $scope.NoRecordFound = true;
                        $scope.MstInstituteProgrammeMapTableParams = new NgTableParams({
                        }, {
                            dataset: [],
                        });
                    }
                    else {

                        $scope.NoRecordFound = false;
                        $scope.MstInstituteProgrammeMapTableParams = new NgTableParams({
                        }, {
                            dataset: response.obj,


                        });
                    }


                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };


    /*Add New Evaluation*/
    $scope.newMstInstituteProgrammeMapAdd = function () {
        $state.go('MstInstituteProgrammeMapAdd');
    };

    /*Back to Edit Page of Evaluation*/
    $scope.backToList = function () {
        $state.go('MstInstituteProgrammeMapAdd');
    };
    $scope.backToList1 = function (Id, InstituteName) {
        $localStorage.InstituteId = Id;
        $localStorage.InstituteName = InstituteName;
        //alert($localStorage.InstituteId);
        $state.go('MstInstituteProgrammeMapEdit');
    };

    $scope.getInstitute = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstInstituteProgrammeMap/MstInstituteGet',

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
                    $scope.MstInstituteTableParams = new NgTableParams({
                        page: 1,
                        count: 200
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };


    $scope.resetUser = function () {
        $scope.user = {};
    }
    $scope.FacultyGet = function () {
        //alert("Faculty Details");
        $http({
            method: 'POST',
            url: 'api/MstInstituteProgrammeMap/FacultyGet',
            data: $scope.MstInstituteProgrammeMap,
            headers: { "Content-Type": 'application/json' }
        })

            .success(function (response) {
                $scope.FacultyList = response.obj;

                //$scope.TestCountry = {
                //};
            })
            .error(function (res) {
                //alert(res);
            });
    };

    $scope.MstProgrammeListGet = function (InstituteId, FacultyId) {

        // $scope.MstInstituteProgrammeMap.FacultyId = $localStorage.facultyDepartIntituteId;

        $scope.checked = {};

        $http({
            method: 'POST',
            url: 'api/MstInstituteProgrammeMap/MstProgrammeGetByFacId',
            data: { InstituteId: $localStorage.InstituteId, FacultyId: $scope.MstInstituteProgrammeMap.FacultyId },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');
                }
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.ProgrammeList = response.obj;
                    $scope.NewProgLst = {};
                    NewProgLst = [];
                    ProgCount = 0;
                    for (key in Object.keys($scope.ProgrammeList)) {
                        if ($scope.ProgrammeList[key].ProgChecked == true) {

                            ProgCount = ProgCount + 1;
                            NewProgLst.push($scope.ProgrammeList[key]);
                        }

                    }
                    $scope.NewProgLst = NewProgLst;


                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };



    $scope.CheckChangeProg = function (MstProgramme, checked) {
        //debugger;
        var i = 100;
        var j = i++ + ++i + ++i;
        console.log($scope.ProgrammeList);
        if (checked) {
            var ProgIndex = NewProgLst.map(function (item) { return item.Id; }).indexOf(MstProgramme.Id);


            if (ProgIndex >= 0) {
                NewProgLst[ProgIndex1].ProgChecked = true;

            }
            else if (ProgIndex < 0) {
                console.log(MstProgramme);
                MstProgramme.ProgChecked = true;
                NewProgLst.push(MstProgramme);

            }

        }
        else if (!(checked)) {
            //debugger;
            //console.log(MstProgramme.ProgrammeId);
            var ProgIndex1 = NewProgLst.map(function (item) { return item.Id; }).indexOf(MstProgramme.Id);
            NewProgLst[ProgIndex1].ProgChecked = false;



        }
        $scope.proglist = new Array();
        console.log(NewProgLst);
        for (var i in NewProgLst) {
            var obj = {};
            obj["ProgrammeId"] = NewProgLst[i].Id;
            obj["InstituteProgrammeMapId"] = NewProgLst[i].InstituteProgrammeMapId;
            obj["ProgChecked"] = NewProgLst[i].ProgChecked;
            $scope.proglist.push(obj);
        }
    };

    /*Add Evaluation*/
    $scope.addMstInstituteProgrammeMap = function () {

        //debugger;
        $scope.MstInstituteProgrammeMap.InstituteId = $localStorage.InstituteId;
        $scope.MstInstituteProgrammeMap.ProgList = $scope.proglist;
        $http({
            method: 'POST',
            url: 'api/MstInstituteProgrammeMap/MstInstituteProgrammeMapAdd',
            data: $scope.MstInstituteProgrammeMap,
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

                    //$scope.MstInstituteProgrammeMap();
                    //$scope.ProgrammeList;
                    $scope.getMstInstituteProgrammeMap();
                    //$scope.MstProgrammeListGet();
                    //$scope.CheckChangeProg();
                    //$scope.CheckChangeProg = {};
                    //$scope.getMstInstituteProgrammeMap = {};
                    //$scope.NewProgLst();
                    //$scope.MstProgrammeListGet = {};



                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });





    };


});
