app.controller('IPTPMCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams, $localStorage) {
    var tokenCookie = $cookies.get('token');
    if (!tokenCookie) {
        localStorage.clear();
        $state.go('login');
    }
    $rootScope.pageTitle = "Institute Part Term Paper Mapping";
    //$localStorage.Institute = {};
    $scope.IPTPM = {};
    $scope.IPTPMTableParams = new NgTableParams({
    }, {
        dataset: [],
    });
    /*Get Institute List*/
    $scope.getInstitute = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/InstitutePartTermPaperMap/MstInstituteGet',

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

    $scope.AddPaper = function (Id, InstituteName) {
       
        $localStorage.Institute = {};
        $localStorage.Institute.Id = Id;
        $localStorage.Institute.InstituteName = InstituteName;
        $state.go('IPTPMEdit');
    };

    /*Back to MstInstituteProgrammeMapAdd*/
    $scope.backToList = function () {
        $state.go('IPTPMAdd');
    };
    /*Academic Year List Get Method */
    $scope.IncAcademicYearListGet = function () {
        
        $scope.IPTPM = {};
        $scope.IPTPM.InstituteId = $localStorage.Institute.Id;
        $scope.IPTPM.InstituteName = $localStorage.Institute.InstituteName;
        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/AcademicYearGet',
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
                        $scope.AcademicList = {};
                    }
                }
                else {
                    $scope.AcademicList = response.obj;                    
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    /* Faculty List Get Method*/
    $scope.FacultyGet = function () {
        $scope.FacultyList = {};
        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/FacultyGet',
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
                        $scope.FacultyList = {};
                    }
                }
                else {
                    $scope.FacultyList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /* Programme List Get Method*/
    $scope.ProgrammeGet = function () {
        $scope.ProgrammeList = {};
        $http({
            method: 'POST',
            url: 'api/InstitutePartTermPaperMap/MstProgrammeGetByFacultyId',
            data: $scope.IPTPM,
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
                        $scope.ProgrammeList = {};
                    }
                }
                else {
                    $scope.ProgrammeList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /* ProgrammePT List Get Method*/
    $scope.PTGet = function () {
        $scope.PTList = {};
        $http({
            method: 'POST',
            url: 'api/InstitutePartTermPaperMap/PartTermGetByProgrammeId',
            data: $scope.IPTPM,
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
                        $scope.PTList = {};
                    }
                }
                else {
                    $scope.PTList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.resetIPTPM = function () {
        $scope.IPTPM = {};
        $scope.IPTPM.InstituteId = $localStorage.Institute.Id;
        $scope.IPTPM.InstituteName = $localStorage.Institute.InstituteName;
    };

    $scope.GetPaperTree = function () {
        debugger;
        $scope.NewObj = $scope.IPTPM;
        $scope.NewObj.ProgrammeInstancePartTermId = $scope.IPTPM.IncProgInstancePartTerm.Id;
        $scope.NewObj.InstancePartTermName = $scope.IPTPM.IncProgInstancePartTerm.InstancePartTermName;
        $http({
            method: 'POST',
            url: 'api/InstitutePartTermPaperMap/GetPaperMapTree',
            data: $scope.NewObj,
            headers: { "Content-Type": 'application/json' }
        })

            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                }
                else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $localStorage.reportData = response.obj;
                    $scope.FetchData();
                    /*Get InstitutePartTermMap*/
                    
                    $scope.InstituteId = $localStorage.InstituteId;
                    $scope.InstituteName = $localStorage.InstituteName;
                    $http({
                        method: 'POST',
                        url: 'api/InstitutePartTermPaperMap/IPTPMGetList',
                        data: $scope.NewObj,
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
                                    $scope.IPTPMTableParams = new NgTableParams({
                                    }, {
                                        dataset: [],
                                    });
                                }
                                else {
                                    $scope.NoRecordFound = false;
                                    $scope.IPTPMTableParams = new NgTableParams({
                                    }, {
                                        dataset: response.obj,
                                    });                                    
                                }
                            }
                        })
                        .error(function (res) {
                            $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                        });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.FetchData = function () {
        $scope.GAPdata = $localStorage.reportData;
        $scope.getTreewithSelect();

    };
    $scope.getTreewithSelect = function () {
        $scope.ShowTree = true;
        $scope.GAPdata.FinalArray = new Array();        
        $('#checkTree').jstree({
            'core': {
                'data': $scope.GAPdata.Parent,
                'themes': {
                    'responsive': false
                }
            },
            'plugins': ['types', 'checkbox'],
            "checkbox": {
                three_state: false,
            },            
        });
        $('#checkTree').on("select_node.jstree", function (e, data) {

            data.node.original.treeid = data.node.id;
            var ParentNodeid = data.node.original.ParentGroupId;
            for (var i in data.selected) {
                if (data.node.id == data.selected[i]) {
                    for (var k in $scope.GAPdata.MinMaxData) {
                        if (data.node.original.IsPaper == true) {
                            if (ParentNodeid == $scope.GAPdata.MinMaxData[k].Id) {
                                $scope.GAPdata.FinalArray.push(data.node.original);
                                $scope.GAPdata.MinMaxData[k].Count = $scope.GAPdata.MinMaxData[k].Count + 1;
                                break;
                            }
                           
                        }
                        
                    }
                }
            }

        });
        $('#checkTree').on("deselect_node.jstree", function (e, data) {
            data.node.original.treeid = data.node.id;
            var ParentNodeid = data.node.original.ParentGroupId;
            for (var i in $scope.GAPdata.FinalArray) {
                if ($scope.GAPdata.FinalArray[i].treeid == data.node.original.treeid) {
                    var originalIndex = $scope.GAPdata.FinalArray.map(function (item) { return item.treeid; }).indexOf(data.node.original.treeid);
                    $scope.GAPdata.FinalArray.splice(originalIndex, 1);
                    for (var k in $scope.GAPdata.MinMaxData) {
                        if (ParentNodeid == $scope.GAPdata.MinMaxData[k].Id) {
                            $scope.GAPdata.MinMaxData[k].Count = $scope.GAPdata.MinMaxData[k].Count - 1;
                        }
                    }
                }
            }

        });
    };
    $scope.SaveTreeData = function () {
        debugger;
        let flag = true;
        $scope.errmsg = 'Kindly Select Paper more than Maximum Condition in listed groups : ';
        debugger;
        for (var i in $scope.GAPdata.FinalArray) {
            var ParentNodeid = $scope.GAPdata.FinalArray[i].ParentGroupId;
            for (var k in $scope.GAPdata.MinMaxData) {
                if ($scope.GAPdata.FinalArray[i].IsPaper == true) {
                    if (($scope.GAPdata.MinMaxData[k].Count < $scope.GAPdata.MinMaxData[k].MaxPapers) && ParentNodeid == $scope.GAPdata.MinMaxData[k].Id) {
                        flag = false;
                        $scope.errmsg = $scope.errmsg + ' ' + $scope.GAPdata.MinMaxData[k].GroupName + ',';
                        break;
                    }
                    else if (ParentNodeid == $scope.GAPdata.MinMaxData[k].Id) { break; }
                }
            }
        }
        if (flag == true) {

            for (var i in $scope.GAPdata.FinalArray) {
                $scope.GAPdata.FinalArray[i].children = {};
            }
            $scope.SaveIPTPM();
        }
        else {
            alert($scope.errmsg);
        }
    };
    $scope.SaveIPTPM = function () {
        $scope.GAPdata.InstituteId = $scope.IPTPM.InstituteId;
        $scope.GAPdata.ProgrammeInstancePartTermId = $scope.IPTPM.ProgrammeInstancePartTermId;
        $http({
            method: 'POST',
            url: 'api/InstitutePartTermPaperMap/SavePaperMapTree',
            data: $scope.GAPdata,
            headers: { "Content-Type": 'application/json' }
        })

            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');
                }
                else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    alert(response.obj);
                }
                else {
                    $scope.ShowTree = false;
                    $scope.ShowTable = false;
                    $scope.ShowInstitute = false;
                    $('#checkTree').jstree('destroy');
                    alert("Paper added Successfully")
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.deleteIPTPM = function (ev, data) {

        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.IPTPM = data;
            $http({
                method: 'POST',
                url: 'api/InstitutePartTermPaperMap/IPTPMDelete',
                data: $scope.IPTPM,
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
                        $scope.ShowTree = false;
                        $('#checkTree').jstree('destroy');
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });

        }, function () {
            $scope.status = 'You decided to keep your debt.';
        });
    };
});