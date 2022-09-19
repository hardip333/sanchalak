app.controller('InstitutePartTermMapCtrl', function ($scope, $localStorage, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $scope.NoRecordFound = false;
    var ProgCount = 0;
    $scope.showSaveBtn = false;
    $rootScope.pageTitle = "Manage InstitutePartTermMap";
    //$scope.checked = {};
    /*Reset Evaluation*/
    $scope.resetInstitutePartTermMap = function () {
        $scope.InstitutePartTermMap = {};
    };

    $scope.getInstitutePartTermMap = {};
    $scope.CheckChangeProgInstPT = {};


    /*Get InstitutePartTermMap*/
    $scope.getInstitutePartTermMap = function () {

        $scope.InstituteId = $localStorage.InstituteId;
        $scope.InstituteName = $localStorage.InstituteName;
        $http({
            method: 'POST',
            url: 'api/InstitutePartTermMap/InstitutePartTermMapGet',
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
                        $scope.InstitutePartTermMapTableParams = new NgTableParams({
                        }, {
                                dataset: [],
                        });
                    }
                    else {

                        $scope.NoRecordFound = false;
                        $scope.InstitutePartTermMapTableParams = new NgTableParams({
                        }, {
                            dataset: response.obj,


                        });
                        //$scope.getInstitute();
                    }


                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Get Institute List*/
    $scope.getInstitute = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/InstitutePartTermMap/MstInstituteGet',

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

                        //$scope.FacultyGet();
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };


    /*Back to MstInstituteProgrammeMapAdd*/
    $scope.backToList = function () {
        $state.go('InstitutePartTermMapAdd');
    };

    /*Proceed to InstitutePartTermMapEdit*/
    $scope.backToList1 = function (Id, InstituteName) {
        $localStorage.InstituteId = Id;
        $localStorage.InstituteName = InstituteName;
        //alert($localStorage.InstituteId);
        $state.go('InstitutePartTermMapEdit');
    };


    $scope.FacultyGet = function () {

        //alert("Faculty Details");
        $http({
            method: 'POST',
            url: 'api/InstitutePartTermMap/FacultyGet',
            data: $scope.InstitutePartTermMap,
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

    $scope.getAcademicList = function () {
        //alert("Faculty Details");
        $http({
            method: 'POST',
            url: 'api/InstitutePartTermMap/AcademicYearGetForDropDown',
            data: $scope.InstitutePartTermMap,
            headers: { "Content-Type": 'application/json' }
        })

            .success(function (response) {
                $scope.AcademicList = response.obj;

                //$scope.TestCountry = {
                //};
            })
            .error(function (res) {
                //alert(res);
            });
    };

    $scope.getProgrammeList = function (FacultyId) {

        $http({
            method: 'POST',
            url: 'api/InstitutePartTermMap/MstProgrammeGetByFacId',
            data: { FacultyId: $scope.InstitutePartTermMap.FacultyId },
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


    $scope.getProgInstPartTermList = function (InstituteId, AcademicYearId, ProgrammeId) {

        $scope.InstitutePartTermMap.InstituteId = $localStorage.InstituteId;
        $scope.contentPresent = false;
        $scope.checked = {};
        $http({
            method: 'POST',
            url: 'api/InstitutePartTermMap/IncProgInstPartTermGetByFacIdAcIdProgId',
            data: { InstituteId: $localStorage.InstituteId, AcademicYearId: $scope.InstitutePartTermMap.AcademicYearId, ProgrammeId: $scope.InstitutePartTermMap.ProgrammeId },
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
                    $scope.ProgInstPartTermList = {};
                }
                else {
                    //debugger;
                    $scope.ProgInstPartTermList = response.obj;
                    $scope.NewProgInstPartTermList = {};
                    NewProgInstPartTermList = [];
                    ProgCount = 0;
                    for (key in Object.keys($scope.ProgInstPartTermList)) {
                        if ($scope.ProgInstPartTermList[key].ProgChecked == true) {

                            ProgCount = ProgCount + 1;
                            NewProgInstPartTermList.push($scope.ProgInstPartTermList[key]);

                        }

                    }
                    $scope.NewProgInstPartTermList = NewProgInstPartTermList;

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };


    $scope.CheckChangeProgInstPT = function (ProgrammeInstancePartTerm, checked) {
        //debugger;
        //var i = 100;
        //var j = i++ + ++i + ++i;
        console.log($scope.ProgInstPartTermList);
        if (checked) {
            var ProgIndex = NewProgInstPartTermList.map(function (item) { return item.Id; }).indexOf(ProgrammeInstancePartTerm.Id);


            if (ProgIndex >= 0) {
                NewProgInstPartTermList[ProgIndex1].ProgChecked = true;

            }
            else if (ProgIndex < 0) {
                console.log(ProgrammeInstancePartTerm);
                ProgrammeInstancePartTerm.ProgChecked = true;
                NewProgInstPartTermList.push(ProgrammeInstancePartTerm);

            }

        }
        else if (!(checked)) {
            //debugger;
            //console.log(MstProgramme.ProgrammeId);
            var ProgIndex1 = NewProgInstPartTermList.map(function (item) { return item.Id; }).indexOf(ProgrammeInstancePartTerm.Id);
            NewProgInstPartTermList[ProgIndex1].ProgChecked = false;



        }
        $scope.proglist = new Array();
        console.log(NewProgInstPartTermList);
        for (var i in NewProgInstPartTermList) {
            var obj = {};
            obj["IncProgInstancePartTermId"] = NewProgInstPartTermList[i].Id;
            obj["InstitutePartTermMapId"] = NewProgInstPartTermList[i].InstitutePartTermMapId;
            obj["ProgChecked"] = NewProgInstPartTermList[i].ProgChecked;
            $scope.proglist.push(obj);
        }
    };

    $scope.addInstitutePartTermMap = function () {

        //debugger;
        $scope.InstitutePartTermMap.InstituteId = $localStorage.InstituteId;
        $scope.InstitutePartTermMap.ProgList = $scope.proglist;
        $http({
            method: 'POST',
            url: 'api/InstitutePartTermMap/InstitutePartTermMapAdd',
            data: $scope.InstitutePartTermMap,
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
                    $scope.getInstitutePartTermMap();
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