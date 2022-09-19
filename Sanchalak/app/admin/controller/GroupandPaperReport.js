app.controller('GAPRCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams, $localStorage) {


    $rootScope.pageTitle = "Group and paper report";
    $scope.Report = {};
    $scope.finalArray = {};
    $scope.ShowTree = false;
    $scope.ShowTreeEdit = false;
    $scope.ShowTable = false;
    $scope.ShowInstitute = false;
    $scope.GAPR = {};
 
    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }

    
    $scope.getFacultyById = function () {
 
        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGetbyId',
            //data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.Institute = response.obj[0];
                $scope.GAPR.FacultyId = $scope.Institute.Id;
                $scope.GAPR.InstituteId = $scope.Institute.InstituteId;
               
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.IncAcademicYearListGet = function () {
        $scope.getFacultyById();
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
                    $scope.showMatrixFlag = false;
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
            url: 'api/GroupAndPaperReport/MstProgrammeGetByFacultyId',
            data: $scope.GAPR,
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
    
    /* Programme List Get Method*/
    $scope.ProgrammeGetbyInstId = function () {
        
        
     
        $scope.ProgrammeList = {};
        $http({
            method: 'POST',
            url: 'api/GroupAndPaperReport/MstProgrammeGetByInstId',
            data: $scope.GAPR,
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
            url: 'api/GroupAndPaperReport/PartTermGetByProgrammeId',
            data: $scope.GAPR,
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
    /* ProgrammePT List Get Method*/
    $scope.PTsGet = function () {
        $scope.PTList = {};
        $http({
            method: 'POST',
            url: 'api/GroupAndPaperReport/ProgrammePartTermListGetByProgrammePartId',
            data: $scope.GAPR,
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
                        $scope.ShowInstitute = false;
                        $scope.ShowTable = false;
                        alert(response.obj);
                    }
                }
                else {
                    $scope.PTList = response.obj;
                    $scope.ShowInstitute = true;
                    $scope.ShowTable = false;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    }; 
    /* Branch List Get Method*/
    $scope.BranchGet = function () {
        $scope.BList = {};
        $http({
            method: 'POST',
            url: 'api/GroupAndPaperReport/MstProgrammeBranchListGetByProgrammePartId',
            data: $scope.GAPR,
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
                        $scope.BList = {};

                    }
                }
                else {
                    $scope.BList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };
    $scope.ProgPartInstListGet = function () {
        $scope.ProgPartList = {};
        $http({
            method: 'POST',
            url: 'api/GroupAndPaperReport/ProgrammePartListGetByProgrammeId',
            data: $scope.GAPR,
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
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    
   
    /* Preference List Get Method*/
    $scope.PreferenceGet = function () {
        $scope.PreferenceList = {};
        $http({
            method: 'POST',
            url: 'api/GroupAndPaperReport/GetPreferenceGroup',
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
                        $scope.PreferenceList = {};

                    }
                }
                else {
                    $scope.PreferenceList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };
    
    $scope.getSR = function () {
        $localStorage.GAPR=$scope.GAPR;
        $state.go('StructureReport');
    };
    
    $scope.getReportList = function () {
        
        $http({
            method: 'POST',
            url: 'api/GroupAndPaperReport/GroupReportGetbyProgrammeId',
            data: $localStorage.GAPR,
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
                    $localStorage.reportData = response.obj.Item1;
                    $scope.reportOTHERdata = response.obj.Item2;
                    $scope.treeCall();

                    
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    $scope.treeCall = function () {
        $scope.treeViewOptions = {
            items: $localStorage.reportData,

        };

    };
    $scope.backToList = function () {
        $state.go('GetGAPReport');
    };

    
    $scope.getAssessmentReportList = function () {
        $http({
            method: 'POST',
            url: 'api/GroupAndPaperReport/AssessmentReportGetbyProgrammeId',
            data: $scope.GAPR,
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
                    $localStorage.AssessmentreportData = response.obj.Item1;
                    $scope.reportOTHERdata = response.obj.Item2;
                    $scope.treeCall1();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    
    $scope.treeCall1 = function () {
        $scope.treeViewOptions = {
            items: $localStorage.AssessmentreportData,

        };

    };
    $scope.printDiv = function (divName, ReportName) {
        var printContents = document.getElementById(divName).innerHTML;
        // CREATE A WINDOW OBJECT.
        var win = window.open('', 'height=700,width=700');
        win.document.write('<html>');
        win.document.write('<head>');
        win.document.write('<title>Programme '+ReportName+' Report</title>');   // <title> FOR PDF HEADER.
        //win.document.write(style);          // ADD STYLE INSIDE THE HEAD TAG.
        win.document.write('<script src="scripts/angular/angular.min.js"></script>');
        win.document.write('<link rel="stylesheet" type="text/css" href="bower_components/bootstrap/css/bootstrap.min.css">');
        win.document.write('<script src="scripts/bootstrap-select/bootstrap-select.min.js"></script>');
        win.document.write('<script src="scripts/angular/ui-bootstrap-tpls-2.0.0.js"></script>');
        win.document.write('<script type="text/javascript" src="bower_components/bootstrap/js/bootstrap.min.js"></script>');
        win.document.write('<script type="text/javascript" src="bower_components/bootstrap-tagsinput/js/bootstrap-tagsinput.js"></script>');
        win.document.write('<script type="text/javascript" src="bower_components/bootstrap-maxlength/js/bootstrap-maxlength.js"></script>');
        win.document.write('</head>');
        win.document.write('<body>');
        win.document.write(printContents);         // THE TABLE CONTENTS INSIDE THE BODY TAG.
        win.document.write('</body></html>');
        win.document.close(); 	// CLOSE THE CURRENT WINDOW.
        setTimeout(function () { win.print(); }, 1000);            // PRINT THE CONTENTS.
        $scope.GAPR = {};
        $localStorage.reportData = {};
    };

   
    /* Student List Get Method*/
    $scope.GetStudent = function () {
        
        $scope.ShowInstitute = false;
        //$scope.GAPR.PTList = $scope.PTList;
        //$scope.GAPR.ProgrammeInstancePartTermId = $scope.GAPR.PTList[0].Id;
        $scope.StudentList = {};
        $scope.onSpinner();
        $http({
            method: 'POST',
            url: 'api/GroupAndPaperReport/StudentGet',
            data: $scope.GAPR,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $scope.offSpinner();
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    if (response.response_code == "201") {
                        $scope.StudentList = {};
                        alert(response.obj);
                        $scope.ShowTable = false;
                    }
                }
                else {
                    $scope.offSpinner();
                    $scope.StudentList = response.obj.Item1;
                    $scope.PTData = response.obj.Item2;
                    if ($scope.PTData.length < 3) {
                        var PTObj = {
                            Id: 0,
                            InstancePartTermName: ""
                        }
                        $scope.PTData.push(PTObj);

                        console.log($scope.PTData);
                    }


                    
                    $scope.ShowTable = true;
                    if ($.fn.dataTable.isDataTable('#StudentDetail')) {
                        $('#StudentDetail').dataTable().fnClearTable();
                        $('#StudentDetail').DataTable().destroy();
                    }             
                    $(document).ready(function () {
                        $('#StudentDetail').DataTable({
                            "bPaginate": true,
                            "paging": true,
                            "ordering": false,
                            "bLengthChange": true,
                            "info": false,
                            "searching": true,
                        });
                    });     
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };
    $scope.GetBulkTree = function () {
        $scope.ShowTable = false;
        $scope.ShowInstitute = true;
        //$scope.GAPR.PTList = $scope.PTList;
        $scope.GAPR.ProgrammeInstancePartTermId = $scope.GAPR.ProgrammeInstancePartTerm.Id;
        $scope.GAPR.InstancePartTermName = $scope.GAPR.ProgrammeInstancePartTerm.InstancePartTermName;
        $http({
            method: 'POST',
            url: 'api/GroupAndPaperReport/GetBulkTreeSelect',
            data: $scope.GAPR,
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
                    //$state.go('GAPSelect');

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };


    $scope.ReportdataFetch = function (SPT, Student) {
        console.log(SPT);
        console.log(Student);

        $scope.PTdatano = SPT;
        var data = {};
        data.PRN = Student.PRN;
        data.NameAsPerMarksheet = Student.NameAsPerMarksheet;
        

        if (SPT == "1") {
            data.Id = Student.PartTerm1;
            data.InstancePartTermName = Student.PartTermName1;
        }
        else if (SPT == "2") {
            data.Id = Student.PartTerm2;
            data.InstancePartTermName = Student.PartTermName2;
        }
        else if (SPT == "3") {
            data.Id = Student.PartTerm3;
            data.InstancePartTermName = Student.PartTermName3;
        }
        debugger;
        $http({
            method: 'POST',
            url: 'api/GroupAndPaperReport/GroupSelectGet',
            data: data,
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
                    //$state.go('GAPSelect');

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
        $('#checkTree').jstree('destroy');
        $scope.ShowTree = true;
        $scope.ShowTreeEdit = false;
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
                            if ($scope.GAPdata.MinMaxData[k].Count < $scope.GAPdata.MinMaxData[k].MaxPapers && ParentNodeid == $scope.GAPdata.MinMaxData[k].Id) {
                                $scope.GAPdata.FinalArray.push(data.node.original);
                                $scope.GAPdata.MinMaxData[k].Count = $scope.GAPdata.MinMaxData[k].Count + 1;
                                break;
                            }
                            else if (ParentNodeid == $scope.GAPdata.MinMaxData[k].Id) {
                                alert("you have selected more paper");
                                var deleteddata = data.selected.splice(0, 1);
                                data.instance.deselect_node(data.node);
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
        let flag = true;
        $scope.errmsg = 'Kindly Select Paper as per Minimum / Maximum Condition in listed groups : ';
        for (var i in $scope.GAPdata.FinalArray) {
            var ParentNodeid = $scope.GAPdata.FinalArray[i].ParentGroupId;
            for (var k in $scope.GAPdata.MinMaxData) {
                if ($scope.GAPdata.FinalArray[i].IsPaper == true) {
                    if (($scope.GAPdata.MinMaxData[k].MinPapers > $scope.GAPdata.MinMaxData[k].Count || $scope.GAPdata.MinMaxData[k].Count > $scope.GAPdata.MinMaxData[k].MaxPapers) && ParentNodeid == $scope.GAPdata.MinMaxData[k].Id) {
                        flag = false;
                        $scope.errmsg = $scope.errmsg + ' ' + $scope.GAPdata.MinMaxData[k].GroupName + ',';
                        break;
                    }
                    else if (ParentNodeid == $scope.GAPdata.MinMaxData[k].Id) { break;}
                }
               
            }            
        }
        if (flag == true) {
         
            for (var i in $scope.GAPdata.FinalArray) {
                $scope.GAPdata.FinalArray[i].children = {};
            }
            if ($scope.ShowTable == true) {     $scope.groupSelectSave();}
            else if ($scope.ShowInstitute == true) {    $scope.groupBulkSelectSave();}
            else { alert("something went wrong, try again"); }
        }
        else {
            alert($scope.errmsg);
        }
    }





    //$scope.groupSelectSave = function () {
      
    //    $http({
    //        method: 'POST',
    //        url: 'api/GroupAndPaperReport/GroupSelectSave',
    //        data: $scope.GAPdata,
    //        headers: { "Content-Type": 'application/json' }
    //    })

    //        .success(function (response) {
    //            $rootScope.showLoading = false;

    //            if (response.response_code == "0") {
    //                $state.go('login');
    //            }
    //            else if (response.response_code != "200") {
    //                $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
    //                alert(response.obj);
    //            }
    //            else {
    //                $scope.ShowTree = false;
    //                $scope.ShowInstitute = false;
    //                $scope.GetStudent();
    //                $('#checkTree').jstree('destroy');
    //                alert("Paper added Successfully")
    //            }
    //        })
    //        .error(function (res) {
    //            $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
    //        });
    //};

    $scope.groupSelectSave = function () {
        var Index1 = $scope.StudentList.map(function (item) { return item.PRN; }).indexOf($scope.GAPdata.PRN);
        console.log(Index1);
        $http({
            method: 'POST',
            url: 'api/GroupAndPaperReport/GroupSelectSave',
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
                    $scope.ShowInstitute = false;
                    $scope.PRN = response.obj.Item1;
                    $scope.PTId = response.obj.Item2;
                    if ($scope.PTdatano == "1") {
                        if ($scope.StudentList[Index1].PRN == $scope.PRN) {
                            if ($scope.StudentList[Index1].PartTerm1 == $scope.PTId) {
                                $scope.StudentList[Index1].PaperSelected1 = true;
                            }                            
                        }
                    }
                    else if ($scope.PTdatano == "2") {
                        if ($scope.StudentList[Index1].PRN == $scope.PRN) {
                            if ($scope.StudentList[Index1].PartTerm2 == $scope.PTId) {
                                console.log($scope.StudentList);
                                $scope.StudentList[Index1].PaperSelected2 = true;
                                console.log($scope.StudentList);
                            }
                        }
                    }
                    else if ($scope.PTdatano == "3") {
                        if ($scope.StudentList[Index1].PRN == $scope.PRN) {
                            if ($scope.StudentList[Index1].PartTerm3 == $scope.PTId) {
                                $scope.StudentList[Index1].PaperSelected3 = true;
                            }
                        }
                    }                    
                    $('#checkTree').jstree('destroy');
                    alert("Paper added Successfully")
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    $scope.groupBulkSelectSave = function () {
        $scope.GAPdata.InstituteId = $scope.GAPR.InstituteId;
        $http({
            method: 'POST',
            url: 'api/GroupAndPaperReport/GroupSelectBulkSave',
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
    /////
    //Edit Paper SELECTION


    $scope.EditPaperdataFetch = function (SPT, Student) {

        var data = {};
        data.PRN = Student.PRN;
        data.NameAsPerMarksheet = Student.NameAsPerMarksheet;


        if (SPT == "1") {
            data.Id = Student.PartTerm1;
            data.InstancePartTermName = Student.PartTermName1;
        }
        else if (SPT == "2") {
            data.Id = Student.PartTerm2;
            data.InstancePartTermName = Student.PartTermName2;
        }
        else if (SPT == "3") {
            data.Id = Student.PartTerm3;
            data.InstancePartTermName = Student.PartTermName3;
        }
        $http({
            method: 'POST',
            url: 'api/GroupAndPaperReport/GroupSelectGetEdit',
            data: data,
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
                    $scope.GAPEditData= response.obj;
                    //$scope.FetchData();
                    //$state.go('GAPSelect');
                    $scope.getTreewithSelectEdit(); 
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    $scope.getTreewithSelectEdit = function () {
        $('#checkTreeEdit').jstree('destroy');
        $scope.ShowTree = false;
        $scope.ShowTreeEdit = true;
        $('#checkTreeEdit').jstree({
            'core': {
                'data': $scope.GAPEditData.Parent,
                'themes': {
                    'responsive': false
                }
            },
            'plugins': ['types', 'checkbox'],
            "checkbox": {
                three_state: false,
            },
        });
        
        $('#checkTreeEdit').on("select_node.jstree", function (e, data) {

            data.node.original.treeid = data.node.id;
            var ParentNodeid = data.node.original.ParentGroupId;
            for (var i in data.selected) {
                if (data.node.id == data.selected[i]) {
                    for (var k in $scope.GAPEditData.MinMaxData) {
                        if (data.node.original.IsPaper == true) {
                            if ($scope.GAPEditData.MinMaxData[k].Count < $scope.GAPEditData.MinMaxData[k].MaxPapers && ParentNodeid == $scope.GAPEditData.MinMaxData[k].Id) {
                                $scope.GAPEditData.FinalArray.push(data.node.original);
                                $scope.GAPEditData.MinMaxData[k].Count = $scope.GAPEditData.MinMaxData[k].Count + 1;
                                break;
                            }
                            else if (ParentNodeid == $scope.GAPEditData.MinMaxData[k].Id) {
                                alert("you have selected more paper");
                                var deleteddata = data.selected.splice(0, 1);
                                data.instance.deselect_node(data.node);
                            }
                        }

                    }
                }
            }
        });
        $('#checkTreeEdit').on("deselect_node.jstree", function (e, data) {
            data.node.original.treeid = data.node.id;
            var ParentNodeid = data.node.original.ParentGroupId;

            for (var i in $scope.GAPEditData.FinalArray) {
                if ($scope.GAPEditData.FinalArray[i].PaperId == data.node.original.PaperId) {
                    var originalIndex = $scope.GAPEditData.FinalArray.map(function (item) { return item.PaperId; }).indexOf(data.node.original.PaperId);
                    $scope.GAPEditData.FinalArray.splice(originalIndex, 1);
                    for (var k in $scope.GAPEditData.MinMaxData) {
                        if (ParentNodeid == $scope.GAPEditData.MinMaxData[k].Id) {
                            $scope.GAPEditData.MinMaxData[k].Count = $scope.GAPEditData.MinMaxData[k].Count - 1;
                        }
                    }
                }
            }
        });
    };

    $scope.SaveTreeEditData = function () {
        let flag = true;
        $scope.errmsg = 'Kindly Select Paper as per Minimum / Maximum Condition in listed groups : ';
        for (var i in $scope.GAPEditData.FinalArray) {
            var ParentNodeid = $scope.GAPEditData.FinalArray[i].ParentGroupId;
            for (var k in $scope.GAPEditData.MinMaxData) {
                if ($scope.GAPEditData.FinalArray[i].IsPaper == true) {
                    if (($scope.GAPEditData.MinMaxData[k].MinPapers > $scope.GAPEditData.MinMaxData[k].Count || $scope.GAPEditData.MinMaxData[k].Count > $scope.GAPEditData.MinMaxData[k].MaxPapers) && ParentNodeid == $scope.GAPEditData.MinMaxData[k].Id) {
                        flag = false;
                        $scope.errmsg = $scope.errmsg + ' ' + $scope.GAPEditData.MinMaxData[k].GroupName + ',';
                        break;
                    }
                    else if (ParentNodeid == $scope.GAPEditData.MinMaxData[k].Id) { break; }
                }

            }
        }
        if (flag == true) {

            for (var i in $scope.GAPEditData.FinalArray) {
                $scope.GAPEditData.FinalArray[i].children = {};
            }
            $scope.groupSelectEditSave();
           
        }
        else {
            alert($scope.errmsg);
        }
    }
    //$scope.groupSelectEditSave = function () {

    //    $http({
    //        method: 'POST',
    //        url: 'api/GroupAndPaperReport/GroupSelectEditSave',
    //        data: $scope.GAPEditData,
    //        headers: { "Content-Type": 'application/json' }
    //    })

    //        .success(function (response) {
    //            $rootScope.showLoading = false;

    //            if (response.response_code == "0") {
    //                $state.go('login');
    //            }
    //            else if (response.response_code != "200") {
    //                $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
    //                alert(response.obj);
    //            }
    //            else {
    //                $scope.ShowTree = false;
    //                $scope.ShowTreeEdit = false;
                    
    //                $scope.ShowInstitute = false;
    //                $scope.GetStudent();
    //                $('#checkTreeEdit').jstree('destroy');
    //                alert("Paper added Successfully")
    //            }
    //        })
    //        .error(function (res) {
    //            $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
    //        });
    //};


    $scope.groupSelectEditSave = function () {
        var Index1 = $scope.StudentList.map(function (item) { return item.PRN; }).indexOf($scope.GAPEditData.PRN);
        $http({
            method: 'POST',
            url: 'api/GroupAndPaperReport/GroupSelectEditSave',
            data: $scope.GAPEditData,
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
                    $scope.ShowTreeEdit = false;

                    $scope.ShowInstitute = false;
                    $scope.PRN = response.obj.Item1;
                    $scope.PTId = response.obj.Item2;
                    if ($scope.StudentList[Index1].PRN == $scope.PRN) {
                        console.log($scope.StudentList[Index1].PRN);
                        for (var i in $scope.StudentList[Index1].PTList) {
                            if ($scope.StudentList[Index1].PTList[i].Id == $scope.PTId) {
                                $scope.StudentList[Index1].PTList[i].IsPaperSelected = true;
                            }
                        }
                    }
                    $('#checkTreeEdit').jstree('destroy');
                    alert("Paper added Successfully")
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };


































    //PGP
    $scope.SelectGroup = function () {

        $http({
            method: 'POST',
            url: 'api/GroupAndPaperReport/GroupPTGet',
            data: $scope.GAPR,
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
                    $scope.TreeselectData();
                    //$state.go('GAPSelect');

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    $scope.TreeselectData = function () {
        $scope.PGPdata = $localStorage.reportData;
        $scope.getTreeSelect();

    };
    $scope.getTreeSelect = function () {
        $scope.ShowTree = true;

        $scope.PGPdata.FinalArray = new Array();
        for (var M in $scope.PGPdata.Parent) {
            $scope.PGPdata.FinalArray.push($scope.PGPdata.Parent[M]);
        }
        $('#PTGTree').jstree({
            'core': {
                'data': $scope.PGPdata.Parent,
                'themes': {
                    'responsive': false
                }
            },
            'types': {
                'default': {
                    'icon': 'icofont icofont-folder'
                },
                'file': {
                    'icon': 'icofont icofont-file-alt'
                }
            },
            'plugins': ['types', 'checkbox'],
            "checkbox": {
                three_state: false,
            },
        });
        //$("#checkTree").jstree('open_all');
        $('#PTGTree').on("select_node.jstree", function (e, data) {
            data.node.original.treeid = data.node.id;
            var ParentNodeid = data.node.original.ParentGroupId;
            for (var i in data.selected) {
                if (data.node.id == data.selected[i]) {
                    for (var k in $scope.PGPdata.MinMaxData) {
                        if ($scope.PGPdata.MinMaxData[k].Count < $scope.PGPdata.MinMaxData[k].MaxSubGroups && ParentNodeid == $scope.PGPdata.MinMaxData[k].Id) {
                            $scope.PGPdata.FinalArray.push(data.node.original);
                            $scope.PGPdata.MinMaxData[k].Count = $scope.PGPdata.MinMaxData[k].Count + 1;
                        }
                        else if (ParentNodeid == $scope.PGPdata.MinMaxData[k].Id) {
                            alert("you have selected more Group");
                            var deleteddata = data.selected.splice(0, 1);
                            data.instance.deselect_node(data.node);        
                        }                        
                    }
                }
            }
          
        });
        $('#PTGTree').on("deselect_node.jstree", function (e, data) {
            data.node.original.treeid = data.node.id;
            var ParentNodeid = data.node.original.ParentGroupId;

            for (var i in $scope.PGPdata.FinalArray) {
                if ($scope.PGPdata.FinalArray[i].treeid == data.node.original.treeid) {
                    var originalIndex = $scope.PGPdata.FinalArray.map(function (item) { return item.treeid; }).indexOf(data.node.original.treeid);
                    $scope.PGPdata.FinalArray.splice(originalIndex, 1);
                    for (var k in $scope.PGPdata.MinMaxData) {
                        if (ParentNodeid == $scope.PGPdata.MinMaxData[k].Id) {
                            $scope.PGPdata.MinMaxData[k].Count = $scope.PGPdata.MinMaxData[k].Count - 1;
                        }
                    }
                }
            }
         
        });
    };
    $scope.GetPGPData = function () {
        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/GroupAndPaperReport/PGPDataListGet',
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
                    $scope.PGPTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    $scope.SavePGPData = function () {
        let flag = true;

        for (var i in $scope.PGPdata.FinalArray) {
            var ParentNodeid = $scope.PGPdata.FinalArray[i].ParentGroupId;
            var ParentIndex = $scope.PGPdata.FinalArray.map(function (item) { return item.Id; }).indexOf(ParentNodeid);
            if (ParentIndex >= 0) {
                for (var k in $scope.PGPdata.MinMaxData) {
                    if ($scope.PGPdata.FinalArray[i].IsPaper == true) {
                        if (($scope.PGPdata.MinMaxData[k].MinPapers > $scope.PGPdata.MinMaxData[k].Count || $scope.PGPdata.MinMaxData[k].Count > $scope.PGPdata.MinMaxData[k].MaxPapers) && ParentNodeid == $scope.PGPdata.MinMaxData[k].Id) {
                            flag = false;
                            alert("Kindly select child as par min max condition");
                        }
                        else if (ParentNodeid == $scope.PGPdata.MinMaxData[k].Id) { break; }
                    }
                    else {
                        if (($scope.PGPdata.MinMaxData[k].MinSubGroups > $scope.PGPdata.MinMaxData[k].Count || $scope.PGPdata.MinMaxData[k].Count > $scope.PGPdata.MinMaxData[k].MaxSubGroups) && ParentNodeid == $scope.PGPdata.MinMaxData[k].Id && $scope.PGPdata.MinMaxData[k].Id != $scope.PGPdata.MinMaxData[k].ParentGroupId) {
                            flag = false;
                            alert("Kindly select child as par min max condition");
                        }
                        else if (ParentNodeid == $scope.PGPdata.MinMaxData[k].Id) { break; }
                    }
                }
            }
            else {
                flag = false;
                alert("Kindly select Parent before child");
            }
        }
        if (flag == true) {
            $scope.PGPSelectSave();
        }

    }
    $scope.PGPSelectSave = function () {
        $scope.PGPdata.PreferenceId = $scope.GAPR.PreferenceId;
        $scope.PGPdata.ProgrammeInstancePartTermId = $scope.GAPR.ProgrammeInstancePartTermId;
        $http({
            method: 'POST',
            url: 'api/GroupAndPaperReport/PGPSelectSave',
            data: $scope.PGPdata,
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
                    $scope.ShowTree = false;
                    
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    }
    $scope.newPGPAdd = function () {
    
        $state.go('PreferencePTGroupMap');
    };
    $scope.backToList = function () {
        $state.go('PreferencePTGroupMapEdit');
    };

    $scope.deletePGP = function (ev, data) {

        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.PGP = data;
            $http({
                method: 'POST',
                url: 'api/GroupAndPaperReport/PGPDelete',
                data: $scope.PGP,
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
                        $scope.GetPGPData();
                        $mdDialog.show(
                            $mdDialog.alert()
                                .parent(angular.element(document.querySelector('#popupContainer')))
                                .clickOutsideToClose(true)
                                .title("Message")
                                .textContent(response.obj)
                                .ariaLabel('Alert Dialog Demo')
                                .ok('Okay!')
                        );
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
            // };
        }, function () {
            $scope.status = 'You decided to keep your debt.';
        });
    };


});