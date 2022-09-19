app.controller('IncProgInstPartTermGroupCtrl', function ($scope, $http, $localStorage, $filter, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {
    //debugger;
    /*Variable Declration */
    $scope.ProgrammeInstancePartTerm = {};
    $scope.ProgInstPartTerm = {};
    $scope.IncProgInstPartTermPaperMap = {};
    $scope.paper = {};
    $scope.showFormFlag = true;
    $scope.GrpFromFlag = true;
    $scope.GrpFromFlag1 = false;
    $scope.ProgInstPartTermGroup = {};
    var LocalObj = {};
    var LocalObject = {};
    $scope.ProgInstPartTermGroupMap = {};
    var NewGroupLst = new Array();
    var PaperCount = 0;
    var GroupCount = 0;
    $scope.flag = false;

    /*Academic Year List Get Method */
    $scope.IncAcademicYearListGet = function () {
        //debugger;
        $http({
            method: 'POST',
            url: 'api/IncProgInstPartTermGroup/IncAcademicYearListGet',
            data: $scope.ProgrammeInstancePartTerm,
            headers: { "Content-Type": 'application/json' }
        })

            .success(function (response) {
                $scope.AcademicList = response.obj;
                if ($localStorage.PartTermDetails.FacultyId) {
                    $scope.FacultyGet();
                }
            })
            .error(function (res) {

            });

    };

    /* Faculty List Get Method*/
    $scope.FacultyGet = function () {

        $scope.FacultyList = {};
        $http({
            method: 'POST',
            url: 'api/IncProgInstPartTermGroup/FacultyGet',
            data: $scope.ProgrammeInstancePartTerm,
            headers: { "Content-Type": 'application/json' }
        })

            .success(function (response) {

                $scope.FacultyList = response.obj;
                if ($localStorage.PartTermDetails.ProgrammeInstanceId) {
                    $scope.getProgrammeInstanceListByAcadId();
                    $scope.getBranchListByProgInstId();
                }

            })
            .error(function (res) {

            });
    };

    /*Programme Instance List By Academic Year Id and Faculty Id */
    $scope.getProgrammeInstanceListByAcadId = function () {
        $scope.InstList = {};

        $http({
            method: 'POST',
            url: 'api/MstProgramInstance/InstanceListGetbyFacultyIdAndAcadId',
            data: $scope.ProgrammeInstancePartTerm,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code != "200") {
                    if (response.response_code == "201") {
                        $scope.InstList = {};
                    }
                    else {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                }
                else {
                    $scope.InstList = response.obj;
                    if ($localStorage.PartTermDetails.ProgrammeInstancePartId) {
                        $scope.getProgrammePartListByProgInstId();
                        $scope.getProgPartTermListByProgInstPartId();
                    }

                }

            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Branch/Specialisation List By Programme Instance Id */
    $scope.getBranchListByProgInstId = function () {

        $scope.BranchList = {};
        $http({
            method: 'POST',
            url: 'api/IncProgInstPartTermGroup/MstProgrammeBranchListGetByProgInstanceId',
            data: $scope.ProgrammeInstancePartTerm,
            headers: { "Content-Type": 'application/json' }
        })

            .success(function (response) {
                $scope.BranchList = response.obj;

            })
            .error(function (res) {

            });
    };

    /*Programme Part List By Programme Instance Id */
    $scope.getProgrammePartListByProgInstId = function () {

        $http({
            method: 'POST',
            url: 'api/IncProgInstPartTermGroup/ProgrammePartGetByProgInstId',
            data: $scope.ProgrammeInstancePartTerm,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgPartList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Programme Part Term List By Programme Instance Id */
    $scope.getProgPartTermListByProgInstPartId = function () {

        $http({
            method: 'POST',
            url: 'api/IncProgInstPartTermGroup/ProgrammePartTermGetByProgInstId',
            data: $scope.ProgrammeInstancePartTerm,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgPartTermList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    /*To show the form to fill the data of Group*/
    $scope.ShowList = function ()
    {
        //debugger;
        if ($scope.ProgrammeInstancePartTerm.AcademicYearId === null || $scope.ProgrammeInstancePartTerm.AcademicYearId === undefined ||
            $scope.ProgrammeInstancePartTerm.FacultyId === null || $scope.ProgrammeInstancePartTerm.FacultyId === undefined ||
            $scope.ProgrammeInstancePartTerm.ProgrammeInstanceId === null || $scope.ProgrammeInstancePartTerm.ProgrammeInstanceId === undefined ||
            $scope.ProgrammeInstancePartTerm.ProgrammeInstancePartId === null || $scope.ProgrammeInstancePartTerm.ProgrammeInstancePartId === undefined ||
            $scope.ProgrammeInstancePartTerm.SpecialisationId === null || $scope.ProgrammeInstancePartTerm.SpecialisationId === undefined ||
            $scope.ProgrammeInstancePartTerm.ProgrammeInstancePartTermId === null || $scope.ProgrammeInstancePartTerm.ProgrammeInstancePartTermId === undefined
        ) {

            alert("please select all fields !!!");
        }
        else {
            $scope.GroupFormFlag = false;
            $scope.GroupFormFlag1 = true;
            $scope.GetGroupData1($scope.ProgrammeInstancePartTerm.ProgrammeInstancePartTermId);
            $scope.ProgrammePartTermGroupListGet();
        }
        $localStorage.PartTermDetails = {};
        $localStorage.PartTermDetails.AcademicYearId = $scope.ProgrammeInstancePartTerm.AcademicYearId;
        $localStorage.PartTermDetails.FacultyId = $scope.ProgrammeInstancePartTerm.FacultyId;
        $localStorage.PartTermDetails.ProgrammeInstanceId = $scope.ProgrammeInstancePartTerm.ProgrammeInstanceId;
        $localStorage.PartTermDetails.ProgrammeInstancePartId = $scope.ProgrammeInstancePartTerm.ProgrammeInstancePartId;
        $localStorage.PartTermDetails.SpecialisationId = $scope.ProgrammeInstancePartTerm.SpecialisationId;
        $localStorage.PartTermDetails.ProgrammeInstancePartTermId = $scope.ProgrammeInstancePartTerm.ProgrammeInstancePartTermId;

    };

    $scope.AddNewGroup = function () {
        //$scope.GetGroupData1($scope.ProgrammeInstancePartTerm.ProgrammeInstancePartTermId);
        $scope.GroupFormFlag = true;
    };


    $scope.GetGroupData1 = function (ProgInstPartTermId) {
        $scope.GroupDataList = {};
        var data = { ProgrammeInstancePartTermId: ProgInstPartTermId };
        
        $http({
            method: 'POST',
            url: 'api/IncProgInstPartTermGroup/GroupListGetByMstProgrammePartTermId',
            data: data,
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
                        $scope.GroupDataList = {};
                    }
                }
                else {
                    $scope.GroupDataList = response.obj;
                }
            })
            .error(function (res) {
                alert(res);
            });
    }

    /*To Add Group in Part Term / Semester*/
    $scope.AddGroup = function () {
        var chkYes = document.getElementById("chkYes");
        if ($scope.ProgInstPartTermGroup.MPTGroupId === null || $scope.ProgInstPartTermGroup.MPTGroupId === undefined ||
            $scope.ProgInstPartTermGroup.ContainsGroup === null || $scope.ProgInstPartTermGroup.ContainsGroup === undefined 

        ) {
            alert("please select all fields !!!");
        }
        else if (chkYes == true) {
           // var VarMaxgrp = $scope.ProgInstPartTermGroup.MaxSubGroups;
           // var VarMingrp = $scope.ProgInstPartTermGroup.MinSubGroups;
            if ($scope.ProgInstPartTermGroup.MaxSubGroups === null || $scope.ProgInstPartTermGroup.MaxSubGroups === undefined ||
                $scope.ProgInstPartTermGroup.MinSubGroups === null || $scope.ProgInstPartTermGroup.MinSubGroups === undefined

            ) {

                alert("Please enter max subgroup(s) and min subgroup(s) details properly !!!");
            }
            else if ($scope.ProgInstPartTermGroup.MaxSubGroups == 0 || $scope.ProgInstPartTermGroup.MinSubGroups == 0) {
                alert("Please enter max subgroup(s) and min subgroup(s) details properly !!!");
            }
            else if (($scope.ProgInstPartTermGroup.MaxSubGroups < $scope.ProgInstPartTermGroup.MinSubGroups) &&
                ($scope.ProgInstPartTermGroup.MaxSubGroups != 0 && $scope.ProgInstPartTermGroup.MinSubGroups != 0)) {
                alert("Minimum SubGroups are not greater than Maximum SubGroups");
            }
        }
        else if (chkYes == false) {
           // var VarMinpaper = $scope.ProgInstPartTermGroup.MinPapers;
           // var VarMaxpaper = $scope.ProgInstPartTermGroup.MaxPapers;
            if ($scope.ProgInstPartTermGroup.MaxPapers === null || $scope.ProgInstPartTermGroup.MaxPapers === undefined ||
                $scope.ProgInstPartTermGroup.MinPapers === null || $scope.ProgInstPartTermGroup.MinPapers === undefined

            ) {
                alert("Please enter max paper(s) and min paper(s) details properly !!!");
            }
            else if ($scope.ProgInstPartTermGroup.MinPapers == 0 || $scope.ProgInstPartTermGroup.MaxPapers == 0) {
                alert("Please enter max paper(s) and min paper(s) details properly !!!");
            }
            else if (($scope.ProgInstPartTermGroup.MaxPapers < $scope.ProgInstPartTermGroup.MinPapers)
                &&
                ($scope.ProgInstPartTermGroup.MaxPapers != 0 && $scope.ProgInstPartTermGroup.MinPapers != 0)) {
                alert("Minimum Papers are not greater than Maximum Papers");
            }
        }


        else {

            $scope.ProgInstPartTermGroup.ProgrammeInstancePartTermId = $scope.ProgrammeInstancePartTerm.ProgrammeInstancePartTermId;
            for (key of Object.keys($scope.GroupDataList)) {
                if ($scope.GroupDataList[key].Id == $scope.ProgInstPartTermGroup.MPTGroupId) {
                    $scope.ProgInstPartTermGroup.GroupName = $scope.GroupDataList[key].GroupName;
                }
            }
            $http({
                method: 'POST',
                url: 'api/IncProgInstPartTermGroup/IncProgInstPartTermGroupAdd',
                data: $scope.ProgInstPartTermGroup,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code == "201") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else if (response.response_code == "202") {
                        alert(response.obj);
                    }
                    else {

                        alert(response.obj.Item1);
                        $scope.ProgrammePartTermGroupListGet();
                        modifyGroupFlag = false;
                        GroupFormFlag = false;
                        $localStorage.GroupLcl = {};
                        $localStorage.GroupLcl.GroupId = response.obj.Item2;
                        $localStorage.GroupLcl.GroupName = response.obj.Item3;
                        $scope.ProgInstPartTermGroup = {};

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Flag for Enable textbox as per radio button*/
    $scope.EnablePaperorGroup = function (flag1) {
        if (flag1) {
            $scope.GrpFromFlag = true;
            $scope.GrpFromFlag1 = false;
        }
        else if (!(flag1)) {

            $scope.GrpFromFlag = false;
            $scope.GrpFromFlag1 = true;
        }

    };

    /*List of Group as per Programme Instance Part Term Id*/
    $scope.ProgrammePartTermGroupListGet = function () {
        //debugger;
        var data = { ProgrammeInstancePartTermId: $scope.ProgrammeInstancePartTerm.ProgrammeInstancePartTermId };

        $http({
            method: 'POST',
            url: 'api/IncProgInstPartTermGroup/IncProgramInstancePartTermGroupGet',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {

                    $scope.ProgInstGroupTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                    console.log("Table");
                    console.log($scope.ProgInstGroupTableParams);
                }

            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Modify Group Data*/
    $scope.modifyGroupData = function (Grpdata) {
        //debugger;
        $scope.GroupFormFlag = true;
        $scope.modifyGroupFlag = true;
         $scope.ProgInstPartTermGroup = Grpdata;
        document.visibilityState($scope.ProgInstPartTermGroup.MPTGroupId) = false;
        //document.visibilityState($scope.ProgInstPartTermGroup.MPTGroupId) = false;
        
        var chkyes = Grpdata.ContainsGroup;
       
        if (chkyes == true) {

            $scope.GrpFromFlag = true;
            $scope.GrpFromFlag1 = false;
           
        } else if (chkyes == false) {

            $scope.GrpFromFlag = false;
            $scope.GrpFromFlag1 = true;
            
        } else {
            alert("oops!!nothing was selected...");
        }
        
    };

    /*Reset Group Data*/
    $scope.CancelGroup = function () {
       $scope.GroupFormFlag1 = true;
        $scope.modifyGroupFlag = false;
        $scope.GroupFormFlag = false;
        $scope.ProgrammeInstancePartTerm.ProgrammeInstancePartTermId = $scope.ProgInstPartTermGroup.ProgrammeInstancePartTermId;
        $scope.ProgInstPartTermGroup = {};
        $scope.ProgrammePartTermGroupListGet(); 
    };

    $scope.ClearSelection = function () {
        $localStorage.PartTermDetails = {};
        $scope.ProgrammeInstancePartTerm = {};
        $scope.GroupFormFlag = false;
        $scope.GroupFormFlag1 = false;
        $scope.PreReqPaperListShow = false;
        $scope.PreReqPaperListInDetailShow = false;
        $scope.FacultyList = {};
        $scope.InstList = {};
        $scope.BranchList = {};
        $scope.ProgPartList = {};
        $scope.ProgPartTermList = {};
    };

    /*Update Group*/
    $scope.ModifyGroup = function () {
        var chkYes = document.getElementById("chkYes");
        if ($scope.ProgInstPartTermGroup.GroupName === null || $scope.ProgInstPartTermGroup.GroupName === undefined ||
            $scope.ProgInstPartTermGroup.ContainsGroup === null || $scope.ProgInstPartTermGroup.ContainsGroup === undefined ||
            $scope.ProgInstPartTermGroup.SeparatePassingHead === null || $scope.ProgInstPartTermGroup.SeparatePassingHead === undefined

        ) {
            alert("please select all fields !!!");
        }
        else if (chkYes == true) {
            //var VarMaxgrp = parseInt($scope.ProgInstPartTermGroup.MaxSubGroups);
            //var VarMingrp = parseInt($scope.ProgInstPartTermGroup.MinSubGroups);
            if ($scope.ProgInstPartTermGroup.MaxSubGroups === null || $scope.ProgInstPartTermGroup.MaxSubGroups === undefined ||
                $scope.ProgInstPartTermGroup.MinSubGroups === null || $scope.ProgInstPartTermGroup.MinSubGroups === undefined

            ) {

                alert("Please enter max subgroup(s) and min subgroup(s) details properly !!!");
            }
            else if (parseInt($scope.ProgInstPartTermGroup.MaxSubGroups) == 0 || parseInt($scope.ProgInstPartTermGroup.MinSubGroups) == 0 ) {
                alert("Please enter max subgroup(s) and min subgroup(s) details properly !!!");
            }
            else if ((parseInt($scope.ProgInstPartTermGroup.MaxSubGroups) < parseInt($scope.ProgInstPartTermGroup.MinSubGroups)) &&
                (parseInt($scope.ProgInstPartTermGroup.MaxSubGroups) != 0 && parseInt($scope.ProgInstPartTermGroup.MinSubGroups) != 0)) {
                alert("Minimum SubGroups are not greater than Maximum SubGroups");
            }
        }
        else if (chkYes == false) {
            //var VarMinpaper = $scope.ProgInstPartTermGroup.MinPapers;
           // var VarMaxpaper = $scope.ProgInstPartTermGroup.MaxPapers;
            if ($scope.ProgInstPartTermGroup.MaxPapers === null || $scope.ProgInstPartTermGroup.MaxPapers === undefined ||
                $scope.ProgInstPartTermGroup.MinPapers === null || $scope.ProgInstPartTermGroup.MinPapers === undefined

            ) {
                alert("Please enter max paper(s) and min paper(s) details properly !!!");
            }
            else if ($scope.ProgInstPartTermGroup.MinPapers == 0 || $scope.ProgInstPartTermGroup.MaxPapers == 0) {
                alert("Please enter max paper(s) and min paper(s) details properly !!!");
            }
            else if (($scope.ProgInstPartTermGroup.MaxPapers < $scope.ProgInstPartTermGroup.MinPapers)
                &&
                ($scope.ProgInstPartTermGroup.MaxPapers != 0 && $scope.ProgInstPartTermGroup.MinPapers != 0)) {
                alert("Minimum Papers are not greater than Maximum Papers");
            }
        }
       
        
        else {
            
            $http({
                method: 'POST',
                url: 'api/IncProgInstPartTermGroup/IncProgInstPartTermGroupUpdate',
                data: $scope.ProgInstPartTermGroup,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    if (response.response_code == "0") {
                        $state.go('login');
                    } else if (response.response_code == "201") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else if (response.response_code == "202") {
                        alert(response.obj);
                    }
                    else if (response.response_code == "200"){
                        alert(response.obj);
                       
                        $scope.GroupFormFlag1 = true;
                        $scope.modifyGroupFlag = false;
                        $scope.GroupFormFlag = false;
                        $scope.ProgrammeInstancePartTerm.ProgrammeInstancePartTermId = $scope.ProgInstPartTermGroup.ProgrammeInstancePartTermId;
                        $scope.ProgInstPartTermGroup = {};
                        $scope.ProgrammePartTermGroupListGet();                        

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }

    };

    /*Display Group Data*/
    $scope.displayGroupData = function (data) {
        $scope.Group = data;
    };

    $scope.backToList = function () {

        if ($localStorage.PartTermDetails) {
            $scope.ProgrammeInstancePartTerm = $localStorage.PartTermDetails;
        }
        $state.go('ProgInstPartTermGroup');
    };

    $scope.AttachGroupPaper = function (data) {
        var LocalObj = data;
        localStorage.setItem('LocalGroupObj', JSON.stringify(LocalObj));
        $state.go('IncProgInstGroupAttachment');
    };

    $scope.GroupOrPaperGet = function () {
        LocalObject = JSON.parse(localStorage.getItem('LocalGroupObj'));

        $scope.ProgInstPartTermGroupMap.InstancePartTermName = LocalObject.InstancePartTermName;
        $scope.ProgInstPartTermGroupMap.GroupName = LocalObject.GroupName;
        $scope.ProgInstPartTermGroupMap.MaxSubGroups = LocalObject.MaxSubGroups;
        $scope.ProgInstPartTermGroupMap.MinSubGroups = LocalObject.MinSubGroups;
        $scope.ProgInstPartTermGroupMap.MaxPapers = LocalObject.MaxPapers;
        $scope.ProgInstPartTermGroupMap.MinPapers = LocalObject.MinPapers;

        if (LocalObject.ContainsGroup == true) {

            $scope.GroupListFormFlag = true;
            $scope.PaperListFormFlag = false;
            $scope.getGroupData();
        }
        else if (LocalObject.ContainsGroup == false) {

            $scope.GroupListFormFlag = false;
            $scope.PaperListFormFlag = true;
            $scope.getPaperData();
        }
    };

    $scope.getGroupData = function () {

        var data = {
            ProgrammeInstancePartTermId: LocalObject.ProgrammeInstancePartTermId,
            ParentGroupId: LocalObject.Id
        };

        $http({
            method: 'POST',
            url: 'api/IncProgInstPartTermGroup/GroupListGetByGroupId',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {

                    $scope.GroupList = response.obj;

                    $scope.NewGroupLst = {};
                    NewGroupLst = [];
                    GroupCount = 0;
                    for (key in Object.keys($scope.GroupList)) {
                        if ($scope.GroupList[key].GroupCheckedSts == true) {
                            GroupCount = GroupCount + 1;
                            NewGroupLst.push($scope.GroupList[key]);
                        }
                    };

                    $scope.NewGroupLst = NewGroupLst;
                }

            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getPaperData = function (GroupId) {

        var data = {
            ProgrammeInstancePartTermId: LocalObject.ProgrammeInstancePartTermId,
            Id: LocalObject.Id
        };

        $http({
            method: 'POST',
            url: 'api/IncProgInstPartTermGroup/PaperListGetByGroupId',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.PaperList = response.obj;

                    $scope.NewPaperLst = {};
                    NewPaperLst = [];
                    PaperCount = 0;
                    for (key in Object.keys($scope.PaperList)) {
                        if ($scope.PaperList[key].PaperCheckedSts == true) {

                            PaperCount = PaperCount + 1;
                            NewPaperLst.push($scope.PaperList[key]);
                        }
                    };

                    $scope.NewPaperLst = NewPaperLst;
                }

            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.CheckChangeGroup = function (Group, checked) {

        if (checked) {
            var GroupIndex = NewGroupLst.map(function (item) { return item.Id; }).indexOf(Group.Id);

            if (GroupIndex >= 0) {
                Group.GroupCheckedSts = true;
                GroupCount = GroupCount + 1;
            }
            else if (GroupIndex < 0) {
                Group.GroupCheckedSts = true;
                GroupCount = GroupCount + 1;
                NewGroupLst.push(Group);
            }

        }
        else if (!(checked)) {

            var GroupIndex1 = NewGroupLst.map(function (item) { return item.Id; }).indexOf(Group.Id);
            NewGroupLst[GroupIndex1].GroupCheckedSts = false;

        }
        $scope.NewGroupLst = {};
        $scope.NewGroupLst = NewGroupLst;

    };

    $scope.CheckChangePaper = function (Paper, checked) {

        var i = 100;
        var j = i++ + ++i + ++i;

        if (checked) {
            var PaperIndex = NewPaperLst.map(function (item) { return item.PaperId; }).indexOf(Paper.PaperId);

            if (PaperIndex >= 0) {
                Paper.PaperCheckedSts = true;
                PaperCount = PaperCount + 1;
            }
            else if (PaperIndex < 0) {
                Paper.PaperCheckedSts = true;
                PaperCount = PaperCount + 1;
                NewPaperLst.push(Paper);

            }

        }
        else if (!(checked)) {

            var PaperIndex1 = NewPaperLst.map(function (item) { return item.PaperId; }).indexOf(Paper.PaperId);

            NewPaperLst[PaperIndex1].PaperCheckedSts = false;
            PaperCount = PaperCount - 1;

        }
        $scope.NewPaperLst = {};
        $scope.NewPaperLst = NewPaperLst;

    };

    $scope.AddParentGroupDetail = function () {

        if (LocalObject.MaxSubGroups <= GroupCount || GroupCount == 0)
        {
            var data1 = {
                ProgrammeInstancePartTermId: LocalObject.ProgrammeInstancePartTermId,
                ParentGroupId: LocalObject.Id,
                GroupList: $scope.NewGroupLst
            }
            $http({
                method: 'POST',
                url: 'api/IncProgInstPartTermGroup/ProgInstPartTermParentGroupUpdate',
                data: data1,
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
                        $scope.NewGroupLst = {};
                        $scope.GroupList = {};
                        $scope.GroupOrPaperGet();

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
        else
        { alert("The Number Of Selected Groups Does not match with Maximum No Of Groups"); }
    };

    $scope.AddPaperGroupDetail = function () {

        if (LocalObject.MaxPapers <= PaperCount || PaperCount == 0 ) 
         {
            var data1 = {
                ProgrammeInstancePartTermId: LocalObject.ProgrammeInstancePartTermId,
                Id: LocalObject.Id,
                PaperList: $scope.NewPaperLst
            }
            $http({
                method: 'POST',
                url: 'api/IncProgInstPartTermGroup/ProgInstPartTermPaperGroupDetailUpdate',
                data: data1,
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
                        $scope.PaperGroupLst = {};
                        $scope.PaperList = {};
                        $scope.GroupOrPaperGet();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
        else
        { alert("The Number Of Selected Papers Does not match with Maximum No Of Papers"); }
    };

    /*Delete Group*/
    $scope.deleteGroup = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.ProgInstPartTermGroup = data;

            $http({
                method: 'POST',
                url: 'api/IncProgInstPartTermGroup/IncProgInstPartTermGroupDelete',
                data: $scope.ProgInstPartTermGroup,
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
                        $scope.ProgrammePartTermGroupListGet();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });

        }, function () {
            $scope.status = 'You decided to keep your debt.';
        });
    };
    //Start region PreRequisitePaperList
    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }	
    //$scope.PreRequisitePaperListGet = function () {
        
    //    if ($scope.ProgrammeInstancePartTerm.ProgrammeInstancePartTermId == "" || $scope.ProgrammeInstancePartTerm.ProgrammeInstancePartTermId == null ||
    //        $scope.ProgrammeInstancePartTerm.ProgrammeInstancePartTermId == undefined || $scope.ProgrammeInstancePartTerm.ProgrammeInstancePartTermId == 0) {

    //        return false;
    //    }
    //    $scope.onSpinner();
    //    var ProgInstPartTermId = {
    //        ProgrammeInstancePartTermId: $scope.ProgrammeInstancePartTerm.ProgrammeInstancePartTermId
    //    } 
    //    $http({
    //        method: 'POST',
    //        url: 'api/PreRequisitePaperList/PreRequisitePaperListGet',
    //        data: ProgInstPartTermId,
    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {
                
    //            $rootScope.showLoading = false;
    //            /*if (response.response_code == "0") {

    //                alert(response.obj);
    //                $state.go("login");
    //            }
    //            else*/
    //            if (response.response_code == "201") {

    //                alert(response.obj);
    //                $scope.offSpinner();
    //            }                
    //            else {
                    
    //                if (response.obj.length == 0) {

    //                    alert("No Record Found!");
    //                    $scope.PreReqPaperListShow = false;
    //                    $scope.PreReqPaperListInDetailShow = false;
    //                    $scope.offSpinner();
    //                }
    //                else {

    //                    $scope.PreRequisitePaperListTableParams = new NgTableParams({
    //                    }, {
    //                        dataset: response.obj
    //                    });
    //                    $scope.PreReqPaperListShow = true;
    //                    $scope.searchCaseResult = response.obj;
    //                    $scope.offSpinner();                        
    //                }                    
    //            }
    //        })
    //        .error(function (res) {

    //            $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
    //        });
    //};
    //$scope.PreRequisitePaperListGetByPTandPaperId = function (PreReqPaperList) {
        
    //    $scope.onSpinner();
    //    $scope.SInstPartTermName = PreReqPaperList.InstancePartTermName;
    //    $scope.SPaperName = PreReqPaperList.PaperName;

    //    var PTandPaperId = {
    //        SourcePartTermId: PreReqPaperList.SourcePartTermId,
    //        SourcePaperId: PreReqPaperList.SourcePaperId
    //    }
    //    $http({
    //        method: 'POST',
    //        url: 'api/PreRequisitePaperList/PreRequisitePaperListGetByPTandPaperId',
    //        data: PTandPaperId,
    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {

    //            $rootScope.showLoading = false;
    //            if (response.response_code == "201") {

    //                alert(response.obj);
    //                $scope.offSpinner();
    //            }
    //            else {
                                        
    //                if (response.obj.length == 0) {

    //                    alert("No Record Found!");
    //                    $scope.PreReqPaperListInDetailShow = false;
    //                    $scope.offSpinner();
    //                }
    //                else {

    //                $scope.PreRequisitePaperListGetByPTandPaperIdTableParams = new NgTableParams({
    //                }, {
    //                    dataset: response.obj
    //                });
    //                $scope.PreReqPaperListInDetailShow = true;
    //                $scope.searchCaseResultFull = response.obj;
    //                setTimeout(setAlert, 1000);
    //                function setAlert() {

    //                    $('html,body').animate({
    //                        scrollTop: $(".second").offset().top
    //                    }, 'slow');
    //                }    
    //                $scope.offSpinner();                    
    //            }
    //            }
    //        })
    //        .error(function (res) {

    //            $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
    //        });
    //};
    //$scope.exportData = function () {
        
    //    alert("Please wait, Excel is being prepared...Do not click again on Export to Excel button until Excel is download");

    //    var LongDate = new Date($.now());
    //    var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
    //    var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
    //    var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

    //    var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
    //    var ExcelFileName = "PreRequisitePaperListShort_" + DateWithoutDashed + time;

    //    var mystyle = {
    //        headers: true,
    //        style: 'font-size:25px;font-weight:bold',
    //        caption: {
    //            title: 'PreRequisite Paper List Short | Date and Time: ' + DateAndTime,
    //        },
    //        columns: [
    //            { columnid: 'IndexId', title: 'Sr.No' },
    //            { columnid: 'InstancePartTermName', title: 'Instance Part Term' },
    //            { columnid: 'PaperName', title: 'Papers' }, 
    //            { columnid: '', title: '' },
    //            { columnid: '', title: '' },
    //            { columnid: '', title: '' }, 
    //        ],
    //    };

    //    //Create XLS format using alasql.js file.
    //    alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.searchCaseResult]);
    //};
    //$scope.exportDataFull = function () {

    //    alert("Please wait, Excel is being prepared...Do not click again on Export to Excel button until Excel is download");

    //    var LongDate = new Date($.now());
    //    var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
    //    var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
    //    var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

    //    var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
    //    var ExcelFileName = "PreRequisitePaperListFull_" + DateWithoutDashed + time;

    //    var mystyle = {
    //        headers: true,
    //        style: 'font-size:25px;font-weight:bold',
    //        caption: {
    //            title: 'PreRequisite Paper List Full | Date and Time: ' + DateAndTime +
    //                '<br>Source Instance Part Term: ' + $scope.SInstPartTermName +
    //                '<br>Source Paper: ' + $scope.SPaperName,
    //        },
    //        columns: [
    //            { columnid: 'PreRequisiteLableName', title: 'PreRequisite Lable' },
    //            { columnid: 'DestiPaperName', title: 'Destination Paper' },
    //            { columnid: 'DestiInstancePartTermName', title: 'Destination Instance Part Term' },
    //            { columnid: 'PreRequisiteTypeName', title: 'PreRequisite Type' },
    //            { columnid: 'Minimum', title: 'Minimum' },
    //            { columnid: 'Maximum', title: 'Maximum' },
    //        ],
    //    };

    //    //Create XLS format using alasql.js file.
    //    alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.searchCaseResultFull]);
    //};
    //End region PreRequisitePaperList
});

app.directive('allowPattern', [allowPatternDirective]);

function allowPatternDirective() {
    return {
        restrict: "A",
        compile: function (tElement, tAttrs) {
            return function (scope, element, attrs) {

                element.bind("keypress", function (event) {
                    var keyCode = event.which || event.keyCode;
                    var keyCodeChar = String.fromCharCode(keyCode);

                    if (!keyCodeChar.match(new RegExp(attrs.allowPattern, "i"))) {
                        event.preventDefault();
                        return false;
                    }

                });
            };
        }
    };
}

