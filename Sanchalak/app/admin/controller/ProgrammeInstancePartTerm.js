app.controller('ProgrammeInstancePartTermCtrl', function ($scope, $localStorage, $http, $localStorage, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {
    $rootScope.pageTitle = "Programme Part Term Detail";
    $scope.ProgInstList = [];
    

    $scope.ProgInstPartTermTableparam = new NgTableParams(
        {}, {
        dataset: $scope.getProgInstList
    });    

    if ($localStorage.define1) {
        $scope.ProgInst = {};
        $scope.ProgInst.FacultyId = $localStorage.define1.FacId;
        $scope.ProgInst.ProgrammeId = $localStorage.define1.ProgId;
        $scope.ProgInst.ProgrammeInstanceId = $localStorage.define1.ProgInstId;
        $scope.ProgInst.AcademicYearId = $localStorage.define1.AcadId;
        $scope.ProgInst.ProgrammePartId = $localStorage.define1.PartId;
    }
    else {
        $localStorage.define1 = null;
    }
    
    $scope.resetProgInstPartTerm = function () {
        
        $scope.ProgInst = {};
    }; 

    $scope.clearlocalstorageterm = function () {
        //localStorage.clear();
        $localStorage.define1 = null;
        $scope.ProgInst = {};

    };

    $scope.getFacultyList = function () {
        
        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGet',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.FacList = response.obj;
               
                //if (localStorage.getItem("ProgId") != null) {

                if ($localStorage.define1.ProgId != null || $localStorage.define1.ProgId != undefined) {    
                                       
                    $scope.getBranchListByProgrammeId();
                    $scope.getProgrammePartListByProgrammeId();
                    
                }                

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getAcademicList = function () {

        $http({
            method: 'POST',
            url: 'api/MstProgramInstance/AcademicYearGet',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.AcadList = response.obj;

                if ($localStorage.define1.AcadId != null || $localStorage.define1.AcadId != undefined) {
                   
                    $scope.getProgrammeInstanceListByAcadId();
                }

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getAdmissionYearList = function () {

        $http({
            method: 'POST',
            url: 'api/MstProgramInstance/AdmissionYearGet',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.AdmYearList = response.obj;

                //if ($localStorage.define1.AcadId != null || $localStorage.define1.AcadId != undefined) {

                //    $scope.getProgrammeInstanceListByAcadId();
                //}

            })
            .error(function (res) {
                alert(res);
            });
    };
   
    $scope.getProgInstPartTermList = function () {
        
        var data = new Object();
        $http({
            method: 'POST',
            url: 'api/MstProgrammeInstancePartTerm/ProgrammeInstancePartTermGet',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {                

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.ProgInstPartTermTableparam = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });                    
                }
            })
            .error(function (res) {
                $rootScope.broadcast('dialog', "Error", "alert", res.obj);
            });
    };   
    
    $scope.getProgrammeInstanceListByAcadId = function () {
        
        $http({
            method: 'POST',
            //url: 'api/MstProgramInstance/InstanceListGetbyFacultyIdAndAcadId',
            url: 'api/MstProgramInstance/MInstanceListGetbyFacultyIdAndAcadId',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                            
                $scope.InstList = response.obj;
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.findProgId = function () {

        if ($scope.ProgInst.ProgrammeId == null || $scope.ProgInst.ProgrammeId == undefined) {
            for (key of Object.keys($scope.InstList)) {
                if ($scope.InstList[key].Id == $scope.ProgInst.ProgrammeInstanceId) {
                    var ProgInstIdlocal = $scope.InstList[key].ProgrammeId;
                    console.log(ProgInstIdlocal);
                }
            }
            $scope.ProgInst.ProgrammeId = ProgInstIdlocal;
            //alert($scope.ProgInst.ProgrammeId);
        }
    };

    $scope.getBranchListByProgrammeId = function () {       
       // console.log("In Branch List");
       // console.log($localStorage.define1);
        
        if ($localStorage.define1 == null || $localStorage.define1 == undefined) {        
            // if (!localStorage.getItem("ProgId")) {           
            $scope.findProgId();        
        }
        else
        {            
            $scope.ProgInst.ProgrammeId = $localStorage.define1.ProgId;
        }
        $http({
            method: 'POST',
            url: 'api/MstProgramInstance/MstProgrammeBranchListGetByProgrammeId',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.BranchList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getProgrammePartListByProgrammeId = function () {

        $http({
            method: 'POST',
            url: 'api/ProgrammeInstancePart/ProgrammePartGetByProgrammeId',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgPartList = response.obj;

                if ($localStorage.define1.PartId != null || $localStorage.define1.PartId != undefined) {
                   $scope.getProgPartTermListByPartId();
                }

            })
            .error(function (res) {
                alert(res);
            });
    };   

    $scope.getProgPartTermListByPartId = function () {
        
        $http({
            method: 'POST',
            url: 'api/MstProgrammeInstancePartTerm/ProgrammePartTermGetByPartId',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgPartTermList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };    

    $scope.progInstPartTermAdd = function () {


        if ($scope.ProgInst.FacultyId == null || $scope.ProgInst.FacultyId === undefined
            || $scope.ProgInst.ProgrammeId == null || $scope.ProgInst.ProgrammeId === undefined
            || $scope.ProgInst.SpecialisationId == null || $scope.ProgInst.SpecialisationId === undefined
            || $scope.ProgInst.AcademicYearId == null || $scope.ProgInst.AcademicYearId === undefined
            || $scope.ProgInst.ProgrammePartId == null || $scope.ProgInst.ProgrammePartId === undefined
            || $scope.ProgInst.ProgrammePartTermId == null || $scope.ProgInst.ProgrammePartTermId === undefined
            || $scope.ProgInst.MaxMarks == null || $scope.ProgInst.MaxMarks === undefined
            || $scope.ProgInst.MinMarks == null || $scope.ProgInst.MinMarks === undefined
            || $scope.ProgInst.MaxPapers == null || $scope.ProgInst.MaxPapers === undefined
            || $scope.ProgInst.MinPapers == null || $scope.ProgInst.MinPapers === undefined
            // || $scope.ProgInst.IsSeparatePassingHead == null || $scope.ProgInst.IsSeparatePassingHead === undefined
        ) {

            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please complete the form before Click...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
		else if (parseInt($scope.ProgInst.MaxMarks) < parseInt($scope.ProgInst.MinMarks)) {
            alert("Minimum Marks are not greater than Maximum Marks");
        }
        else if (parseInt($scope.ProgInst.MaxPapers) < parseInt($scope.ProgInst.MinPapers)) {
            alert("Minimum Papers are not greater than Maximum Papers");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstProgrammeInstancePartTerm/ProgrammeInstancePartTermAdd',
                data: $scope.ProgInst,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        alert(response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.ProgInst = {};
                        $scope.getProgInstPartTermList();
                    }
                })
                .error(function (res) {
                    alert(res.obj);
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.modifyProgInstPartTermdata = function (data) {
        $scope.showFormFlag = true;
        $scope.ProgInst = data;
        if (!($scope.AcadList == null && $scope.AcadList == undefined)) { $scope.getAdmissionYearList (); }
        if (!($scope.getFacultyList == null && $scope.getFacultyList == undefined)) { $scope.getProgrammeInstanceListByAcadId(); }
        if (!($scope.getProgrammeInstanceListByAcadId == null && $scope.getProgrammeInstanceListByAcadId == undefined)) { $scope.getBranchListByProgrammeId(); }
        if (!($scope.getBranchListByProgrammeId == null && $scope.getBranchListByProgrammeId == undefined)) { $scope.getProgrammePartListByProgrammeId(); }
        if (!($scope.getProgrammePartListByProgrammeId == null && $scope.getProgrammePartListByProgrammeId == undefined)) { $scope.getProgPartTermListByPartId(); }
        $(window).scrollTop(0);
    };

    $scope.modifyProgInstPartTerm = function () {


            if ($scope.ProgInst.FacultyId == null || $scope.ProgInst.FacultyId === undefined
                || $scope.ProgInst.ProgrammeId == null || $scope.ProgInst.ProgrammeId === undefined
                || $scope.ProgInst.SpecialisationId == null || $scope.ProgInst.SpecialisationId === undefined
                || $scope.ProgInst.AcademicYearId == null || $scope.ProgInst.AcademicYearId === undefined
                || $scope.ProgInst.ProgrammePartId == null || $scope.ProgInst.ProgrammePartId === undefined
                || $scope.ProgInst.ProgrammePartTermId == null || $scope.ProgInst.ProgrammePartTermId === undefined
                || $scope.ProgInst.MaxMarks == null || $scope.ProgInst.MaxMarks === undefined
                || $scope.ProgInst.MinMarks == null || $scope.ProgInst.MinMarks === undefined
                || $scope.ProgInst.MaxPapers == null || $scope.ProgInst.MaxPapers === undefined
                || $scope.ProgInst.MinPapers == null || $scope.ProgInst.MinPapers === undefined
                //|| $scope.ProgInst.IsSeparatePassingHead == null || $scope.ProgInst.IsSeparatePassingHead === undefined
            ) {

                $mdDialog.show(
                    $mdDialog.alert()
                        .parent(angular.element(document.querySelector('#popupContainer')))
                        .clickOutsideToClose(true)
                        .title("Error")
                        .textContent("Please complete the form before Click...")
                        .ariaLabel('Alert Dialog Demo')
                        .ok('Okay!')
                );
            }
			else if (parseInt($scope.ProgInst.MaxMarks) < parseInt($scope.ProgInst.MinMarks)) {
				alert("Minimum Marks are not greater than Maximum Marks");
			}
			else if (parseInt($scope.ProgInst.MaxPapers) < parseInt($scope.ProgInst.MinPapers)) {
				alert("Minimum Papers are not greater than Maximum Papers");
			}
            else {

                $http({
                    method: 'POST',
                    url: 'api/MstProgrammeInstancePartTerm/ProgrammeInstancePartTermUpdate',
                    data: $scope.ProgInst,
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {

                        if (response.response_code == "0") {
                            $state.go('login');

                        } else if (response.response_code != "200") {
                            alert(response.obj);
                            $rootScope.broadcast('dialog', "Error", "alert", response.obj);
                        } else {
                            alert(response.obj);
                            $scope.ProgInst = {};
                            $scope.showFormFlag = false;
                            $scope.getProgInstPartTermList();
                        }
                    })
                    .error(function (res) {
                        alert(res.obj);
                        $rootScope.broadcast('dialog', "Error", "alert", res.obj);
                    });
            }
    };

    $scope.deleteProgInstPartTerm = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.ProgInst = data;

            $http({
                method: 'POST',
                url: 'api/MstProgrammeInstancePartTerm/ProgrammeInstancePartTermDelete',
                data: $scope.ProgInst,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {

                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {  
                        alert(response.obj);
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.getProgInstPartTermList();    
                    }
                    
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
            
        }, function () {
                $scope.status = 'You decided to keep your debt.';
                alert($scope.status);
        });
    };
   
    $scope.showProgInstPartTerm = function (data) {
        
        $scope.newProgInst = data;
        
        $http({
            method: 'POST',
            url: 'api/MstProgrammeInstancePartTerm/ProgrammeInstancePartTermIsActiveEnable',
            data: $scope.newProgInst,
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
                    $scope.getProgInstPartTermList();
                   }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
       

    };

    $scope.hideProgInstPartTerm = function (data) {

        $scope.newProgInst = data;
        
        $http({
            method: 'POST',
            url: 'api/MstProgrammeInstancePartTerm/ProgrammeInstancePartTermIsActiveDisable',
            data: $scope.newProgInst,
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
                    $scope.getProgInstPartTermList();
                    
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.newProgInstPartTermAdd = function () {
        $state.go('ProgrammeInstancePartTermAdd');
    };

    $scope.backToList = function () {
        //localStorage.clear();
        $localStorage.define1 = null;
        $state.go('ProgrammeInstancePartTermEdit');
    };

    $scope.displayProgInstPartTerm = function (data) {
        $scope.ProgInst = data;
    };

});

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

