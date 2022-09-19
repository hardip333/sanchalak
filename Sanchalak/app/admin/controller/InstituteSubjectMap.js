app.controller('InstituteSubjectMapCtrl', function ($scope, $localStorage, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $scope.NoRecordFound = false;
    var ProgCount = 0;
    $scope.showSaveBtn = false;
    $rootScope.pageTitle = "Manage InstituteSubjectMap";
    //$scope.checked = {};
    /*Reset Evaluation*/
    $scope.resetInstituteSubjectMap = function () {
        $scope.InstituteSubjectMap = {};
    };

    $scope.getInstituteSubjectMap = {};
    $scope.CheckChangeSub = {};

    /*Get Evaluation List*/
    $scope.getInstituteSubjectMap = function (InstituteId) {

        $scope.InstituteId = $localStorage.InstituteId;        
        $scope.InstituteName = $localStorage.InstituteName;
        
        $http({
            method: 'POST',
            url: 'api/InstituteSubjectMap/InstituteSubjectMapGet',
            data: { InstituteId: $localStorage.InstituteId},
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
                        $scope.InstituteSubjectMapTableParams = new NgTableParams({
                        }, {
                            dataset: [],
                        });
                    }
                    else {

                        $scope.NoRecordFound = false;
                        $scope.InstituteSubjectMapTableParams = new NgTableParams({
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

    /*Back to Edit Page of Evaluation*/
    $scope.backToList = function () {
        $state.go('InstituteSubjectMapAdd');
    };
    $scope.backToList1 = function (Id, InstituteName) {
        $localStorage.InstituteId = Id;
        $localStorage.InstituteName = InstituteName;
        //alert($localStorage.InstituteId);
        $state.go('InstituteSubjectMapEdit');
    };

    $scope.getInstitute = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/InstituteSubjectMap/MstInstituteGet',

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
        //alert($localStorage.InstituteId);
        $http({
            method: 'POST',
            url: 'api/InstituteSubjectMap/FacultyGetByInstituteId',
            data: { InstituteId: $localStorage.InstituteId },
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

    $scope.getProgrammeList = function () {
        //alert("Faculty Details");
        $http({
            method: 'POST',
            url: 'api/InstituteSubjectMap/MstProgrammeGetByFacultyId',
            data: { FacultyId: $scope.InstituteSubjectMap.FacultyId },
            headers: { "Content-Type": 'application/json' }
        })

            .success(function (response) {
                $scope.ProgrammeList = response.obj;

                //$scope.TestCountry = {
                //};
            })
            .error(function (res) {
                //alert(res);
            });
    };

    $scope.GetSpecialisationList = function () {
        //alert("Faculty Details");
        $http({
            method: 'POST',
            url: 'api/InstituteSubjectMap/MstSpecialisationGetByPId',
            data: { ProgrammeId: $scope.InstituteSubjectMap.ProgrammeId },
            headers: { "Content-Type": 'application/json' }
        })

            .success(function (response) {
                $scope.SpecList = response.obj;

                //$scope.TestCountry = {
                //};
            })
            .error(function (res) {
                //alert(res);
            });
    };

    $scope.SubjectListGet = function (InstituteId, FacultyId,ProgrammeId,SpecialisationId) {

        // $scope.MstInstituteProgrammeMap.FacultyId = $localStorage.facultyDepartIntituteId;

        $scope.checked = {};

        $http({
            method: 'POST',
            url: 'api/InstituteSubjectMap/MstSubjectGetByFId',
            data: {
                InstituteId: $localStorage.InstituteId, FacultyId: $scope.InstituteSubjectMap.FacultyId,
                ProgrammeId: $scope.InstituteSubjectMap.ProgrammeId, SpecialisationId: $scope.InstituteSubjectMap.SpecialisationId
            },
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
                    $scope.SubjectList = response.obj;
                    $scope.NewSubLst = {}; //NewProgLst
                    NewSubLst = []; //NewProgLst
                    SubCount = 0; //ProgCount
                    //ProgrammeList[key]  ProgChecked ProgrammeList
                    for (key in Object.keys($scope.SubjectList)) {
                        if ($scope.SubjectList[key].SubChecked == true)  { 

                            SubCount = SubCount + 1; //ProgCount
                            //NewProgLst.push($scope.ProgrammeList[key]);
                            NewSubLst.push($scope.SubjectList[key]);
                        }

                    }
                        //$scope.NewProgLst = NewProgLst;
                    $scope.NewSubLst = NewSubLst;


                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.CheckChangeSub = function (MstSubject, checked) {
        //debugger;
        var i = 100;
        var j = i++ + ++i + ++i;
        console.log($scope.SubjectList);
        if (checked) {
            var SubIndex = NewSubLst.map(function (item) { return item.Id; }).indexOf(MstSubject.Id); //NewProgLst


            if (SubIndex >= 0) {
                //NewProgLst[ProgIndex1].ProgChecked = true;
                NewSubLst[SubIndex1].SubChecked = true;
            }
            else if (SubIndex < 0) {
                console.log(MstSubject);
                MstSubject.SubChecked = true; //ProgChecked
                //NewProgLst.push(MstProgramme);
                NewSubLst.push(MstSubject);

            }

        }
        else if (!(checked)) {
            //debugger;
            //console.log(MstProgramme.ProgrammeId);
            var SubIndex1 = NewSubLst.map(function (item) { return item.Id; }).indexOf(MstSubject.Id);
            NewSubLst[SubIndex1].SubChecked = false;
            //var ProgIndex1 = NewProgLst.map(function (item) { return item.Id; }).indexOf(MstProgramme.Id);
            //NewProgLst[ProgIndex1].ProgChecked = false;
            
        }
        //debugger
        $scope.SubList = new Array(); //proglist
        console.log(NewSubLst); //NewProgLst
        for (var i in NewSubLst) {
            var obj = {};
            obj["SubjectId"] = NewSubLst[i].Id; //NewProgLst[i].Id
            obj["InstituteSubjectMapId"] = NewSubLst[i].InstituteSubjectMapId; //NewProgLst[i].InstituteProgrammeMapId
            obj["SubChecked"] = NewSubLst[i].SubChecked;
            //obj["FacultyId"] = NewSubLst[i].FacultyId;
            //obj["ProgrammeId"] = NewSubLst[i].ProgrammeId;
            //obj["SpecialisationId"] = NewSubLst[i].SpecialisationId;
            //obj["ProgChecked"] = NewSubLst[i].SubChecked;
            $scope.SubList.push(obj);
        }
    };

    //$scope.CheckChangeProg = function (MstProgramme, checked) {
    //    //debugger;
    //    var i = 100;
    //    var j = i++ + ++i + ++i;
    //    console.log($scope.ProgrammeList);
    //    if (checked) {
    //        var ProgIndex = NewProgLst.map(function (item) { return item.Id; }).indexOf(MstProgramme.Id);


    //        if (ProgIndex >= 0) {
    //            NewProgLst[ProgIndex1].ProgChecked = true;

    //        }
    //        else if (ProgIndex < 0) {
    //            console.log(MstProgramme);
    //            MstProgramme.ProgChecked = true;
    //            NewProgLst.push(MstProgramme);

    //        }

    //    }
    //    else if (!(checked)) {
    //        //debugger;
    //        //console.log(MstProgramme.ProgrammeId);
    //        var ProgIndex1 = NewProgLst.map(function (item) { return item.Id; }).indexOf(MstProgramme.Id);
    //        NewProgLst[ProgIndex1].ProgChecked = false;



    //    }
    //    $scope.proglist = new Array();
    //    console.log(NewProgLst);
    //    for (var i in NewProgLst) {
    //        var obj = {};
    //        obj["ProgrammeId"] = NewProgLst[i].Id;
    //        obj["InstituteProgrammeMapId"] = NewProgLst[i].InstituteProgrammeMapId;
    //        obj["ProgChecked"] = NewProgLst[i].ProgChecked;
    //        $scope.proglist.push(obj);
    //    }
    //};

    /*Add InstituteSubjectMap*/
    $scope.addInstituteSubjectMap= function () {

        //debugger
        $scope.InstituteSubjectMap.InstituteId = $localStorage.InstituteId;
        $scope.InstituteSubjectMap.SubList = $scope.SubList;
        $http({
            method: 'POST',
            url: 'api/InstituteSubjectMap/InstituteSubjectMapAdd',
            data: $scope.InstituteSubjectMap,
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
                    $scope.getInstituteSubjectMap();
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

    $scope.backToGet = function () {
        $state.go('InstituteSubjectMapAdd');
    }

});
