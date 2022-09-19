app.controller('PrereqCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams, $localStorage) {

    $rootScope.pageTitle = "PreRequisite - Manage";
    $scope.ShowDestPaperListflag = false;
    $scope.DisableMinMax = false;
    $scope.ShowMinMax = false;
    $scope.GetPaperList = function () {
        $scope.PaperList = {};
        $http({
            method: 'POST',
            url: 'api/Prerequisite/GetSourcePaperListByPTId',
            data: $scope.filter,
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
                        $scope.PaperList = {};
                        alert(response.obj);
                    }
                }
                else {
                    $scope.PaperList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getPrereqType = function () {
        $scope.PrerequisiteTypeList = {};
        $http({
            method: 'POST',
            url: 'api/MstPreRequisiteType/MstPreRequisiteTypeGet',
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
                        $scope.PrerequisiteTypeList = {};
                        alert(response.obj);
                    }
                }
                else {
                    $scope.PrerequisiteTypeList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    
    

    $scope.getPartTermPaperList = function () {
        $scope.ShowDestPaperListflag = false;
        $scope.Prereq = {};
        $scope.Prereq.SourceIncPaperId = $scope.filter.SourcePaper.Id;
        $scope.Prereq.SourcePaperId = $scope.filter.SourcePaper.SourcePaperId;
        $scope.Prereq.SourcePaperName = $scope.filter.SourcePaper.SourcePaperName;
        $scope.Prereq.SourcePartTermId = $scope.filter.SourcePaper.SourcePartTermId;
        $scope.Prereq.SourcePartTermName = $scope.filter.SourcePaper.SourcePartTermName;
        $scope.Prereq.PrerequisiteTypeId = $scope.filter.PrerequisiteType.Id;
        $scope.Prereq.PreRequisiteTypeName = $scope.filter.PrerequisiteType.PreRequisiteTypeName;

        $scope.PartTermPaperList = {};
        $http({
            method: 'POST',
            url: 'api/Prerequisite/GetDestinationPaperListByPTIdPaperId',
            data: $scope.Prereq,
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
                        $scope.PartTermPaperList = {};
                        $scope.ShowDestPaperListflag = false;
                        alert(response.obj);
                    }
                }
                else {
                    $scope.PartTermPaperList = response.obj;
                    $scope.ShowDestPaperListflag = true;
                    if ($scope.Prereq.PreRequisiteTypeName == "Any of This") {
                        $scope.ShowMinMax = true;
                    }
                    else {
                        $scope.ShowMinMax = false;
                    }
                    $scope.Minimum = 0;
                    $scope.Maximum = 0;
                    for (var j in $scope.PartTermPaperList) {
                        
                        if (Boolean($scope.PartTermPaperList[j].DestinationPaperSelected) == true) {
                            if ($scope.PartTermPaperList[j].PreRequisiteTypeNameCurrent == "Any of This") {
                                $scope.Minimum = $scope.PartTermPaperList[j].Minimum;
                                $scope.Maximum = $scope.PartTermPaperList[j].Maximum;
                            }
                        }
                    }
                    if ($scope.Minimum > 0 || $scope.Maximum > 0) {
                        $scope.DisableMinMax = true;
                    }
                    else {
                        $scope.DisableMinMax = false;
                    }
                   
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.CheckType = function () {
        for (var j in $scope.PartTermPaperList) {

            if (Boolean($scope.PartTermPaperList[j].DestinationPaperSelected) == true) {
                if ($scope.PartTermPaperList[j].PrerequisiteTypeName == "Any of This") {
                    $scope.PartTermPaperList[j].enableminmax = false;
                }
            }
            else {
                $scope.PartTermPaperList[j].enableminmax = true;

            }
        }
    };

   


    $scope.SubmitWithinPrereq = function () {
       
        for (var j in $scope.PartTermPaperList) {

            if (Boolean($scope.PartTermPaperList[j].DestinationPaperSelected) == true) {
                if ($scope.Prereq.PreRequisiteTypeName == "Any of This") {
                    $scope.PartTermPaperList[j].Minimum = $scope.Minimum;
                    $scope.PartTermPaperList[j].Maximum = $scope.Maximum;
                }
            }
        }

        $http({
            method: 'POST',
            url: 'api/Prerequisite/AddPrereqWithin',
            data: $scope.PartTermPaperList,
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
                        
                        alert(response.obj);
                    }
                }
                else {
                    $scope.PartTermPaperList = {};
                    $scope.ShowDestPaperListflag = false;
                    alert("Prerequisite Added");
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };


    $scope.getAcrossPartTermPaperList = function () {

        $scope.Prereq = {};
        $scope.Prereq.SourceIncPaperId = $scope.filter.SourcePaper.Id;
        $scope.Prereq.SourcePaperId = $scope.filter.SourcePaper.SourcePaperId;
        $scope.Prereq.SourcePaperName = $scope.filter.SourcePaper.SourcePaperName;
        $scope.Prereq.SourcePartTermId = $scope.filter.SourcePaper.SourcePartTermId;
        $scope.Prereq.SourcePartTermName = $scope.filter.SourcePaper.SourcePartTermName;
        $scope.Prereq.PrerequisiteTypeId = $scope.filter.PrerequisiteType.Id;
        $scope.Prereq.PreRequisiteTypeName = $scope.filter.PrerequisiteType.PreRequisiteTypeName;
        $scope.PartTermAcrossPaperList = {};
        $scope.ShowDestPaperListflag = false;
        $http({
            method: 'POST',
            url: 'api/Prerequisite/GetDestinationAccrossPaperListByPTIdPaperId',
            data: $scope.Prereq,
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
                        $scope.PartTermPaperList = {};
                        $scope.ShowDestPaperListflag = false;
                        alert(response.obj);
                    }
                }
                else {
                    $scope.PartTermAcrossPaperList = response.obj;
                    $scope.ShowDestPaperListflag = true;
                    if ($scope.Prereq.PreRequisiteTypeName == "Any of This") {
                        $scope.ShowMinMax = true;
                    }
                    else {
                        $scope.ShowMinMax = false;
                    }
                    $scope.Minimum = 0;
                    $scope.Maximum = 0;
                    for (var i in $scope.PartTermAcrossPaperList) {
                        for (var j in $scope.PartTermAcrossPaperList[i].PaperPrereqList) {
                            if (Boolean($scope.PartTermAcrossPaperList[i].PaperPrereqList[j].DestinationPaperSelected) == true) {
                                if ($scope.PartTermAcrossPaperList[i].PaperPrereqList[j].PreRequisiteTypeNameCurrent == "Any of This") {
                                    $scope.Minimum = $scope.PartTermAcrossPaperList[i].PaperPrereqList[j].Minimum;
                                    $scope.Maximum = $scope.PartTermAcrossPaperList[i].PaperPrereqList[j].Maximum;
                                }
                            }                            
                        }
                    }
                    if ($scope.Minimum > 0 || $scope.Maximum > 0) {
                        $scope.DisableMinMax = true;
                    }
                    else {
                        $scope.DisableMinMax = false;
                    }
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    }



    $scope.CheckTypeacross = function () {
        for (var i in $scope.PartTermAcrossPaperList) {


            for (var j in $scope.PartTermAcrossPaperList[i].PaperPrereqList) {

                if (Boolean($scope.PartTermAcrossPaperList[i].PaperPrereqList[j].DestinationPaperSelected) == true) {
                    if ($scope.PartTermAcrossPaperList[i].PaperPrereqList[j].PrerequisiteTypeName == "Any of This") {
                        $scope.PartTermAcrossPaperList[i].PaperPrereqList[j].enableminmax = false;
                    }
                }
                else {
                    $scope.PartTermAcrossPaperList[i].PaperPrereqList[j].enableminmax = true;

                }
            }
        }
    };

    
    $scope.SubmitAcrossPrereq = function () {
        for (var i in $scope.PartTermAcrossPaperList) {
            for (var j in $scope.PartTermAcrossPaperList[i].PaperPrereqList) {
                if (Boolean($scope.PartTermAcrossPaperList[i].PaperPrereqList[j].DestinationPaperSelected) == true) {
                    if ($scope.Prereq.PreRequisiteTypeName == "Any of This") {
                        $scope.PartTermAcrossPaperList[i].PaperPrereqList[j].Minimum = $scope.Minimum;
                        $scope.PartTermAcrossPaperList[i].PaperPrereqList[j].Maximum = $scope.Maximum;
                    }
                }
            }
        }


        $http({
            method: 'POST',
            url: 'api/Prerequisite/AddPrereqAcross',
            data: $scope.PartTermAcrossPaperList,
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

                        alert(response.obj);
                    }
                }
                else {
                    $scope.PartTermAcrossPaperList = {};
                    $scope.ShowDestPaperListflag = false;
                    alert("Prerequisite Added");
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getPrereqList = function () {
        $scope.ShowDestPaperListflag = false;
        $scope.Prereq = {};
        $scope.Prereq.SourceIncPaperId = $scope.filter.SourcePaper.Id;
        $scope.Prereq.SourcePaperId = $scope.filter.SourcePaper.SourcePaperId;
        $scope.Prereq.SourcePaperName = $scope.filter.SourcePaper.SourcePaperName;
        $scope.Prereq.SourcePartTermId = $scope.filter.SourcePaper.SourcePartTermId;
        $scope.Prereq.SourcePartTermName = $scope.filter.SourcePaper.SourcePartTermName;
        $scope.PrereqList = {};
        $scope.PRTableParams = new NgTableParams({
        }, {
            dataset: []
        });

        $http({
            method: 'POST',
            url: 'api/Prerequisite/GetPrereqList',
            data: $scope.Prereq,
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
                        $scope.PartTermPaperList = {};
                        $scope.ShowDestPaperListflag = false;
                        alert(response.obj);
                    }
                }
                else {
                    $scope.PrereqList = response.obj;
                    $scope.PRTableParams = new NgTableParams({
                    }, {
                        dataset: $scope.PrereqList
                    });
                    $scope.ShowDestPaperListflag = true;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    
    $scope.DeletePrereq = function (data) {

            $http({
                method: 'POST',
                url: 'api/Prerequisite/DeletePrereq',
                data: data,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    if (response.response_code == "0") {
                        $state.go('login');
                    }

                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        $mdDialog.show(
                            $mdDialog.alert()
                                .parent(angular.element(document.querySelector('#popupContainer')))
                                .clickOutsideToClose(true)
                                .title("Message")
                                .textContent(response.obj)
                                .ariaLabel('Alert Dialog Demo')
                                .ok('Okay!')
                        );
                        $scope.PrereqList = {};
                        $scope.PRTableParams = new NgTableParams({
                        }, {
                            dataset: []
                        });
                        $scope.ShowDestPaperListflag = false;
                        $scope.getPrereqList()
                        
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });

     
    };

    

});