app.controller('PreProgrammeInstancePartCtrl', function ($scope, $http, $rootScope, $window, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {
    $rootScope.pageTitle = "Programme Instance Part Detail";
    $scope.ProgInstList = [];
    $scope.InstanceNameList = [];
    $scope.ProgInst = {};
    //$scope.ProgInstPartTableparam = new NgTableParams(
    //    {}, {
    //    dataset: $scope.ProgInstList
    //});


    if ($localStorage.localObj.flagfromInstance == 1) {
       
        /*--This is the flag for identify user move from Programme Instance to Programme Instance Part--*/
        $localStorage.localObj.flagfromInstance = 0;
        /*--This is the flag for Disable selected Part value--*/
        $scope.partDisable = true;
    }
    else {
      
        $localStorage.PreProgInstData = {};
        $localStorage.localObj = {};
        $scope.ProgInst = {};
        /*--This is the flag for Enable selected Part value--*/
        $scope.partDisable = false;
    }

    //PreProgramInstancePartConfiguration
    $scope.newPreProgInstPartConfigAdd = function () {
        //alert("In");
        $state.go('PreProgInstancePartConfigAdd');
    };
    $scope.resetProgInstPart = function () {
        $scope.ProgInst = {};
    };

    $scope.clearlocalstorage = function () {
        $localStorage.PreProgInstData = {};
        $localStorage.localObj = {};
        $scope.ProgInst = {};
        /*--This is the flag for Enable selected Part value--*/
        $scope.partDisable = false;

    };

  /*  $scope.getProgrammeList = function () {

        $http({
            method: 'POST',
            url: 'api/MstProgramInstance/MstProgrammeListGet',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgList = response.obj;
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getProgrmLst = function () {
        $http({
            method: 'POST',
            url: 'api/MstProgramme/MstProgrammeListGet',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgIList = response.obj;
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
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

            })
            .error(function (res) {
                alert(res);
            });
    };
    //$scope.getFacultyList();
    */

    $scope.getFacultyById = function () {
        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGetbyId',
          //  data: $scope.ProgInst,
            data: $scope.ProgInst,

            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.Institute = response.obj[0]; // Archana madam's code
                console.log($scope.Institute);
                //$scope.Faculty = response.obj; // Krunal's code               
             //   $scope.ProgInst.FacultyId = $scope.Institute.Id;
               // $scope.getPreProgrammeList();
                //$scope.getProgInstPartList();
                $scope.getPreProgInstPartListByFacultyId();
                $scope.getgetInstanceNameList();

            })
            .error(function (res) {
                alert(res);
            });
    };
    $scope.backToList = function () {
        $state.go('PreProgInstancePartConfigEdit');
    };
    $scope.preProgInstPartAdd = function () {
        //alert("In");
        if ($localStorage.localObj.ProgrammeInstance == null) {
            
            $localStorage.localObj.ProgrammeInstance = $scope.ProgInst.ProgrammeInstanceId;
            $scope.getProgrammeInstanceProgIdAcaId($scope.ProgInst.ProgrammeInstanceId);
        }
        if (
            //$scope.ProgInst.ProgrammeId == null || $scope.ProgInst.ProgrammeId === undefined
            //|| $scope.ProgInst.SpecialisationId == null || $scope.ProgInst.SpecialisationId === undefined
            //|| $scope.ProgInst.AcademicYearId == null || $scope.ProgInst.AcademicYearId === undefined
            $scope.ProgInst.ProgrammePartId == null || $scope.ProgInst.ProgrammePartId === undefined
         //   || $scope.ProgInst.MaxMarks == null || $scope.ProgInst.MaxMarks === undefined
            //  || $scope.ProgInst.MinMarks == null || $scope.ProgInst.MinMarks === undefined
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
        else {
            if ($scope.ProgInst.MaxMarks1 == null || $scope.ProgInst.MaxMarks1 === undefined) { $scope.ProgInst.MaxMarks1 = null; }
            if ($scope.ProgInst.MinMarks1 == null || $scope.ProgInst.MinMarks1 === undefined) { $scope.ProgInst.MinMarks1 = null; }
            $http({
                method: 'POST',
                url: 'api/ProgrammeInstancePart/PreProgrammeInstancePartAdd',
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

                        // alert(response.obj);
                        alert(response.obj.strMessage);
                        /*alert(response.obj.ProgInstancePart);
                        alert(response.obj.ProgrammePartName);*/
                        if (response.obj.ProgInstancePart > 0) {
                            if ($localStorage.localObj) {
                                $localStorage.localObj.ProgrammeInstancePart = response.obj.ProgInstancePart;
                                $localStorage.localObj.PartName = response.obj.ProgrammePartName;
                                $localStorage.localObj.ProgrammePartId = $scope.ProgInst.ProgrammePartId;
                                /*--This is the flag for identify user move from Part--*/
                                $localStorage.localObj.flagfromPart = 1;
                                /*--This is the flag for identify user move from Programme Instance to Programme Instance Part--*/
                                // $localStorage.localObj.flagfromInstance = 0;
                            } else {
                                $localStorage.localObj = {};
                                $localStorage.localObj.ProgrammeInstancePart = response.obj.ProgInstancePart;
                                $localStorage.localObj.ProgrammeInstancePart = response.obj.ProgInstancePart;
                                $localStorage.localObj.ProgrammePartId = $scope.ProgInst.ProgrammePartId;
                                /*--This is the flag for identify user move from Part--*/
                                $localStorage.localObj.flagfromPart = 1;
                                /*--This is the flag for identify user move from Programme Instance to Programme Instance Part--*/
                                // $localStorage.localObj.flagfromInstance = 0;
                            }
                            // console.log($localStorage.ProgrammeInstancePart);
                            $scope.ProgInst = {};
                            //$scope.getProgInstPartList();
                            $scope.getPreProgInstPartListByFacultyId();
                            $state.go('PreProgInstancePartTermConfigAdd');
                        }
                        else {
                            $scope.ProgInst = {};
                        }
                    }
                })
                .error(function (res) {
                    alert(res.obj);
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.getgetInstanceNameList = function () {

        //  var facultyId = { FacultyId: $scope.Faculty.Id }
        var InstituteId = { InstituteId: $scope.Institute.InstituteId }
        $http({
            method: 'POST',
            url: 'api/MstProgramInstance/PreMstProgrammeInstanceListGetbyFacultyId',
            data: InstituteId,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    //$scope.ProgInstTableparam = new NgTableParams(
                    //    {}, {
                    //    dataset: response.obj

                    //});
                    $scope.InstanceNameList = response.obj;
                    //   console.log("soni", JSON.stringify($scope.ProgInst))
                    //  console.log($scope.InstanceNameList);

                  //  $scope.ProgInst.ProgrammeInstanceId = $localStorage.localObj.ProgrammeInstance;
                    //console.localObj($scope.ProgInst.ProgrammeInstanceId);
                    

                    $scope.ProgInst.ProgrammeInstanceId = $localStorage.localObj.ProgrammeInstance;
                    $scope.getProgrammePartListByProgrammeId();

                    //if (this.showFormFlag == false) {
                    //    $scope.ProgInst.ProgrammeInstanceId = $localStorage.PreProgInstData.ProgrammeInstanceId;
                    //}

                }
            })
            .error(function (res) {
                $rootScope.broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getPreProgInstPartListByFacultyId = function () {

        var InstituteId = { InstituteId: $scope.Institute.InstituteId }

        $http({
            method: 'POST',
            url: 'api/ProgrammeInstancePart/PreProgrammePartGetByFacultyId',
            data: InstituteId,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                  //  $rootScope.broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    //$scope.ProgInstPartListByFacultyId = response.obj;
                    $scope.ProgInstPartByFacultyIdTableparam = new NgTableParams(
                        {}, {
                        dataset: response.obj
                    });

                }
            })
            .error(function (res) {
                $rootScope.broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getProgrammePartListByProgrammeId = function () {
        console.log("===========");
        console.log($scope.ProgInst.ProgrammeInstanceId);
        console.log("*********************");
        console.log($scope.InstanceNameList);
        console.log("===========");
        for (key of Object.keys($scope.InstanceNameList)) {
            if ($scope.InstanceNameList[key].Id == $scope.ProgInst.ProgrammeInstanceId) {
              var  ProgId = $scope.InstanceNameList[key].ProgrammeId;
                //console.log(ProgId);
            }
        }
        //$scope.ProgInst.ProgrammeId;
        $scope.ProgInst.ProgrammeId = ProgId;

        // $scope.ProgPartList = {};
        $http({
            method: 'POST',
            url: 'api/ProgrammeInstancePart/ProgrammePartGetByProgrammeId',
            data: $scope.ProgInst,
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


        $scope.getPreProgrammeList = function () {
            var InstituteId = { InstituteId: $scope.Institute.InstituteId }
            //var XML = { FacultyId: $scope.Faculty.Id }
            $http({
                method: 'POST',
                url: 'api/MstProgramInstance/MstProgrammeListGet',
                data: InstituteId,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $scope.ProgList = response.obj;
                    //console.log($scope.ProgList);
                    $scope.ProgInst.ProgrammeInstanceId = $localStorage.localObj.ProgrammeInstance;
                    //$scope.getgetInstanceNameList();
                    $scope.getBranchListByProgrammeId();
                    $scope.getProgrammePartListByProgrammeId();
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
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
                    $scope.ProgInst.AcademicYearId = $localStorage.PreProgInstData.AcademicYearId;

                })
                .error(function (res) {
                    alert(res);
                });
        };

        //$scope.getProgInstPartList = function () {

        //    var data = new Object();
        //    $http({
        //        method: 'POST',
        //        url: 'api/ProgrammeInstancePart/ProgrammeInstancePartGet',
        //        data: data,
        //        headers: { "Content-Type": 'application/json' }
        //    })
        //        .success(function (response) {
        //            if (response.response_code == "0") {
        //                $state.go('login');

        //            } else if (response.response_code != "200") {
        //                $rootScope.broadcast('dialog', "Error", "alert", response.obj);
        //            }
        //            else {
        //                $scope.ProgInstPartTableparam = new NgTableParams(
        //                    {}, {
        //                    dataset: response.obj
        //                });

        //            }
        //        })
        //        .error(function (res) {
        //            $rootScope.broadcast('dialog', "Error", "alert", res.obj);
        //        });
        //};
        //$scope.getProgInstPartList();


        $scope.getBranchListByProgrammeId = function () {
            // alert("hello");
            $http({
                method: 'POST',
                url: 'api/MstProgramInstance/MstProgrammeBranchListGetByProgrammeId',
                data: $scope.ProgInst,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $scope.BranchList = response.obj;
                    $scope.ProgInst.SpecialisationId = $localStorage.PreProgInstData.SpecialisationId;

                })
                .error(function (res) {
                    alert(res);
                });
        };

        $scope.getBranchList = function () {

            $http({
                method: 'POST',
                url: 'api/MstProgramInstance/MstProgrammeBranchListGet',
                data: $scope.ProgInst,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $scope.BranchList1 = response.obj;

                })
                .error(function (res) {
                    alert(res);
                });
        };



        $scope.getProgrammePartList = function () {

            $http({
                method: 'POST',
                url: 'api/ProgrammeInstancePart/ProgrammePartGet',
                data: $scope.ProgInst,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $scope.ProgPartList = response.obj;

                })
                .error(function (res) {
                    alert(res);
                });
        };



        $scope.progInstPartAdd = function () {

            if ($scope.ProgInst.FacultyId == null || $scope.ProgInst.FacultyId === undefined
                || $scope.ProgInst.ProgrammeId == null || $scope.ProgInst.ProgrammeId === undefined
                || $scope.ProgInst.SpecialisationId == null || $scope.ProgInst.SpecialisationId === undefined
                || $scope.ProgInst.AcademicYearId == null || $scope.ProgInst.AcademicYearId === undefined
                || $scope.ProgInst.ProgrammePartId == null || $scope.ProgInst.ProgrammePartId === undefined
                || $scope.ProgInst.MaxMarks == null || $scope.ProgInst.MaxMarks === undefined
                || $scope.ProgInst.MinMarks == null || $scope.ProgInst.MinMarks === undefined) {

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
            else {

                $http({
                    method: 'POST',
                    url: 'api/ProgrammeInstancePart/ProgrammeInstancePartAdd',
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
                            //$scope.getProgInstPartList();
                            $scope.getPreProgInstPartListByFacultyId();
                        }
                    })
                    .error(function (res) {
                        alert(res.obj);
                        $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                    });
            }
        };

        $scope.getProgrammeInstanceProgIdAcaId = function (InstanceId) {
            $http({
                method: 'POST',
                url: 'api/MstProgramInstance/PreMstProgrammeInstanceListGetbyId',
                data: { Id: InstanceId },
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    //$scope.ProgPartList1 = response.obj;
                    $localStorage.PreProgInstData.ProgrammeId = response.obj[0].ProgrammeId;
                    $localStorage.PreProgInstData.AcademicYearId = response.obj[0].AcademicYearId;
                })
                .error(function (res) {
                    alert(res);
                });
        };

        
        $scope.modifyProgInstPartdata = function (data) {

            $scope.showFormFlag = true;
            $scope.ProgInst = data;
            console.log(data);
            $localStorage.localObj.ProgrammeInstance = $scope.ProgInst.ProgrammeInstanceId;
            $scope.getgetInstanceNameList();

        };

        $scope.modifyProgInstPart = function () {

            if (
                //$scope.ProgInst.FacultyId == null || $scope.ProgInst.FacultyId === undefined
                //|| $scope.ProgInst.ProgrammeId == null || $scope.ProgInst.ProgrammeId === undefined
                //|| $scope.ProgInst.SpecialisationId == null || $scope.ProgInst.SpecialisationId === undefined
                //|| $scope.ProgInst.AcademicYearId == null || $scope.ProgInst.AcademicYearId === undefined
                $scope.ProgInst.ProgrammePartId == null || $scope.ProgInst.ProgrammePartId === undefined
                || $scope.ProgInst.MaxMarks == null || $scope.ProgInst.MaxMarks === undefined
                || $scope.ProgInst.MinMarks == null || $scope.ProgInst.MinMarks === undefined) {

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
            else {

                $http({
                    method: 'POST',
                    url: 'api/ProgrammeInstancePart/PreProgrammeInstancePartUpdate',
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
                            //$scope.getProgInstPartList();
                            $scope.getPreProgInstPartListByFacultyId();

                        }
                    })
                    .error(function (res) {
                        alert(res.obj);
                        $rootScope.broadcast('dialog', "Error", "alert", res.obj);
                    });
            }
        };

        $scope.deleteProgInstPart = function (ev, data) {
            var confirm = $mdDialog.confirm()
                .title("Would you like to delete?")
                .textContent('')
                .ariaLabel('Lucky Day')
                .targetEvent(ev)
                .ok('Yes')
                .cancel('No');
            $mdDialog.show(confirm).then(function () {
                $scope.ProgInst = data;
                $http({
                    method: 'POST',
                    url: 'api/ProgrammeInstancePart/ProgrammeInstancePartDelete',
                    data: data,
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        if (response.response_code == "0") {
                            $state.go('login');

                        } else if (response.response_code != "200") {
                            alert(response.obj);
                            $rootScope.broadcast('dialog', "Error", "alert", response.Object);
                        }
                        else {
                            alert(response.obj);
                            //$scope.getProgInstPartList();
                            $scope.getPreProgInstPartListByFacultyId();
                        }
                    })
                    .error(function (res) {
                        $rootScope.broadcast('dialog', "Error", "alert", res.obj);
                    });

            }, function () {
                $scope.status = 'You decided not to delete your data.';
                alert($scope.status);
            });
        };

        $scope.showProgInstPart = function (data) {

            $scope.newProgInstPart = data;

            $http({
                method: 'POST',
                url: 'api/ProgrammeInstancePart/ProgrammeInstancePartIsActiveEnable',
                data: $scope.newProgInstPart,
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
                        //$scope.getProgInstPartList();
                        $scope.getPreProgInstPartListByFacultyId();
                        // $scope.InactiveFlag = false;

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });


        };

        $scope.hideProgInstPart = function (data) {

            //  data.createdById = $rootScope.id;
            $scope.newProgInstPart = data;
            // $scope.InactiveFlag = false;
            $http({
                method: 'POST',
                url: 'api/ProgrammeInstancePart/ProgrammeInstancePartIsActiveDisable',
                data: $scope.newProgInstPart,
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
                        //$scope.getProgInstPartList();
                        $scope.getPreProgInstPartListByFacultyId();
                        //  $scope.InactiveFlag = true;

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });

        };

        $scope.newProgInstPartAdd = function () {
            $state.go('ProgrammeInstancePartAdd');
        };

       

        $scope.displayProgInstPart = function (data) {
            console.log(data);
            $scope.ProgInst = data;
        };
        

        $scope.nextAdd = function () {
            $state.go('PreProgInstancePartTermConfigEdit');
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
