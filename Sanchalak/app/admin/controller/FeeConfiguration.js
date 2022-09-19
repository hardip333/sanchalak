app.controller('FEECONFIGCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, $localStorage, NgTableParams) {
    var tokenCookie = $cookies.get('token');
    if (!tokenCookie) {
        localStorage.clear();
        $state.go('login');
    }
    /*Variable Declration */
    $localStorage.FEECONFIGPrint = {};
    $scope.FEECONFIG = {};
    $scope.FCPTMAP = {};
    $scope.FeeCopy = {};
    $scope.VerifyFlag = false;
    $scope.Showbuttonflag = false;
    //$localStorage.FEECONFIG = {};
    var newCategorylist = new Array();
    var AmountList = new Array();
    $scope.FeeVerifyData = {};

    $rootScope.pageTitle = "Manage Fee Configuration";
    $scope.FEECONFIGTableParams = new NgTableParams({
    }, {
        dataset: $scope.FEECONFIGList
    });
    $scope.PIPTtableParams = new NgTableParams({
    }, {
        dataset: $scope.PIPTList
    });
    $scope.FCPTMAP = new NgTableParams({
    }, {
        dataset: $scope.publishMatrixList
    });
    $scope.resetFEECONFIG = function () {
        $scope.FEECONFIG = {};
    };

    //Get Method code start
    $scope.getFEECONFIGList = function () {

        //var data = new Object();

        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/FeeConfigurationGet',
            //data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.showtable = false;
                    $scope.FEECONFIGTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                    $scope.showMatrixFlag = false;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    /*Academic Year List Get Method */
    $scope.IncAcademicYearListGet = function () {
        //alert("Faculty Details");
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







    /* ProgrammeLevel List Get Method*/
    $scope.ProgrammeLavelGet = function () {
        $scope.PLList = {};
        $http({
            method: 'POST',
            url: 'api/MstProgrammeLevel/MstProgrammeLevelListGet',
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
                        $scope.PLList = {};

                    }
                }
                else {
                    $scope.PLList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };





    /*Programme Instance List By Academic Year Id and Faculty Id */
    $scope.getProgrammeInstanceListByAcadId = function () {
        $scope.InstList = {};
        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/ProgrammeGetbyFacultyIdAndAcadId',
            data: $scope.FEECONFIG,
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
                        $scope.InstList = {};

                    }
                }
                else {
                    $scope.InstList = response.obj;
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
            url: 'api/FeeConfiguration/MstSpecialisationGetByPId',
            data: $scope.FEECONFIG,
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
                        $scope.BranchList = {};

                    }
                }
                else {
                    $scope.BranchList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    /*Programme Part List By Programme Instance Id */
    $scope.getProgrammePartListByProgInstId = function () {
        //$scope.FEECONFIG.ProgrammeInstanceId = $scope.FEECONFIG.ProgrammeInstanceId;


        $scope.ProgPartList = {};
        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/MstProgrammePartGetByProgrammeIdAndProgInstId',
            data: $scope.FEECONFIG,
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
    /*Programme Part Term List By Programme Instance Id */
    $scope.getProgPartTermListByProgInstPartId = function () {
        $scope.ProgPartTermList = {};
        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/ProgrammePartTermGetByProgInstId',
            data: $scope.FEECONFIG,
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
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };



    $scope.getFTList = function () {
        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstFeeType/MstFeeTypeGet',
            //data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.FTList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    $scope.getFCList = function () {
        var data = new Object();

        $scope.FEECONFIG = {};
        $scope.FEECONFIG.AcademicYearId = $localStorage.FEECONFIG.AcademicYearId;
        $scope.FEECONFIG.FeeTypeId = $localStorage.FEECONFIG.FeeTypeId;

        $http({
            method: 'POST',
            url: 'api/FeeTypeFeeCategoryMap/FeeCategoryGetbyFeeTypeId',
            data: $scope.FEECONFIG,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.FeeTypeName = $localStorage.FEECONFIG.FeeTypeName;
                    $scope.PartTermShortName = $localStorage.FEECONFIG.PartTermShortName;
                    $scope.FCList = response.obj;
                    for (var i in $scope.FCList) {
                        var amt2Obj = {};
                        amt2Obj["FeeCategoryId"] = $scope.FCList[i].FeeCategoryId;
                        amt2Obj["IsInstalmentGiven"] = false;
                        amt2Obj["FeeCat"] = false;
                        amt2Obj["FeeState"] = true;
                        amt2Obj["NoOfInstalment"] = 0;
                        amt2Obj["FeeCategoryName"] = $scope.FCList[i].FeeCategoryName;

                        newCategorylist.push(amt2Obj);
                    }
                    $scope.newCategorylist = newCategorylist;
                    $scope.showtable = false;
                    

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    $scope.getFHList = function () {

        $http({
            method: 'POST',
            url: 'api/MstFeeHead/MstFeeHeadGet',
            //data: $scope.FEECONFIG,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.FHList = response.obj;

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    $scope.getFSHList = function () {

        $scope.FEECONFIG = {};
        $scope.FEECONFIG.FeeTypeId = $localStorage.FEECONFIG.FeeTypeId;
        $scope.FEECONFIG.AcademicYearId = $localStorage.FEECONFIG.AcademicYearId;

        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/MstFeeSubHeadGetbyFHId',
            data: $scope.FEECONFIG,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.FSHList = response.obj;
                    var newFClist = new Array();
                    for (var j in $scope.newCategorylist) {
                        if ($scope.newCategorylist[j].FeeCat == true) {

                            newFClist.push($scope.newCategorylist[j]);
                        }
                    }
                    $scope.newFClist = newFClist;
                    $scope.MakeMatrix();

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.NextPage = function () {
        $localStorage.FEECONFIG = {};
        $localStorage.FEECONFIG.ProgrammeInstancePartTermId = $scope.FEECONFIG.ProgrammeInstancePartTermId;
        $localStorage.FEECONFIG.AcademicYearId = $scope.FEECONFIG.AcademicYearId;
        $localStorage.FEECONFIG.FeeTypeId = $scope.FEECONFIG.FeeTypeId;
        for (var i in $scope.ProgPartTermList) {
            if ($localStorage.FEECONFIG.ProgrammeInstancePartTermId == $scope.ProgPartTermList[i].Id) {
                $localStorage.FEECONFIG.PartTermShortName = $scope.ProgPartTermList[i].PartTermShortName;
            }
        }
        for (var j in $scope.FTList) {
            if ($localStorage.FEECONFIG.FeeTypeId == $scope.FTList[j].Id) {
                $localStorage.FEECONFIG.FeeTypeName = $scope.FTList[j].FeeTypeName;
            }
        }
        $state.go('FEECONFIGAddNext');
    };

    $scope.CheckChange = function (FC, checked) {

        if (checked) {
            newCategorylist.push(FC);
        }
        else {
            alert("in");
            newCategorylist = newCategorylist.filter(function (item) {

                alert(item + "---" + FC.FeeCategoryId);
                return item.Id != FC.FeeCategoryId;
            });
        }
        $scope.newCategorylist = newCategorylist;


    };
    $scope.CheckFee = function () {

        for (var j in $scope.newCategorylist) {

            if (Boolean($scope.newCategorylist[j].FeeCat) == true) {
                $scope.newCategorylist[j].FeeState = false;
            }
            else {
                $scope.newCategorylist[j].FeeState = true;
                $scope.newCategorylist[j].FeeCat = false;
                $scope.newCategorylist[j].IsInstalmentGiven = false;
                $scope.newCategorylist[j].NoOfInstalment = 0;

            }
        }
    };
    $scope.MakeMatrix = function () {
        let count = 0;
        let countTotal = 0;
        var TotalAmount = new Array();
        for (var FeeCategory in $scope.newFClist) {
            var amt1Obj = {};
            amt1Obj["TotalFee"] = 0;
            amt1Obj["Id"] = $scope.newFClist[FeeCategory].FeeCategoryId;
            amt1Obj["IsInstalmentGiven"] = $scope.newFClist[FeeCategory].IsInstalmentGiven;
            amt1Obj["NoOfInstalment"] = $scope.newFClist[FeeCategory].NoOfInstalment;
            TotalAmount.push(amt1Obj);
            countTotal = countTotal + 1;
        }
        $scope.FEECONFIG.TotalAmountList = TotalAmount;

        var arrData = new Array();
        for (var FeeSubHead in $scope.FSHList) {
            var obj = {};
            var Amount = new Array();

            obj["Id"] = $scope.FSHList[FeeSubHead].FeeSubHeadId;
            obj["FeeSubHeadName"] = $scope.FSHList[FeeSubHead].FeeSubHeadName;
            obj["FeeHeadId"] = $scope.FSHList[FeeSubHead].FeeHeadId;
            obj["FeeHeadName"] = $scope.FSHList[FeeSubHead].FeeHeadName;
            obj["FeeSubHeadSrno"] = $scope.FSHList[FeeSubHead].FeeSubHeadSrno;
            obj["AllowsNegativeValue"] = $scope.FSHList[FeeSubHead].AllowsNegativeValue;
            obj["IsActive"] = $scope.FSHList[FeeSubHead].IsActive;
            for (var FeeCategory in $scope.newFClist) {
                var amtObj = {};
                amtObj["Id"] = $scope.newFClist[FeeCategory].FeeCategoryId;
                if ($scope.newFClist[FeeCategory].IsInstalmentGiven == true) {
                    amtObj["InstalmentGivenCheck"] = false;
                }
                else {
                    amtObj["InstalmentGivenCheck"] = true;
                }
                amtObj["IsInstalmentAllowed"] = false;
                amtObj["FeeAmount"] = 0;
                Amount.push(amtObj);
                count = count + 1;
            }
            obj["Amount"] = Amount;

            arrData.push(obj);
        }
        $scope.FEECONFIG.AmountList = arrData;
        $scope.FEECONFIG.Count = count;
        $scope.FEECONFIG.countTotal = countTotal;
        $scope.showtable = true;
    };
    $scope.TotalFee = function () {
        try {
            for (var TotalAmount in $scope.FEECONFIG.TotalAmountList) {
                $scope.FEECONFIG.TotalAmountList[TotalAmount].TotalFee = 0
            }
            for (var FeeSubHead in $scope.FEECONFIG.AmountList) {
                for (var Fee in $scope.FEECONFIG.AmountList[FeeSubHead].Amount) {
                    for (var TotalAmount in $scope.FEECONFIG.TotalAmountList) {
                        if ($scope.FEECONFIG.TotalAmountList[TotalAmount].Id === $scope.FEECONFIG.AmountList[FeeSubHead].Amount[Fee].Id) {
                            $scope.FEECONFIG.TotalAmountList[TotalAmount].TotalFee = parseFloat($scope.FEECONFIG.TotalAmountList[TotalAmount].TotalFee) + parseFloat($scope.FEECONFIG.AmountList[FeeSubHead].Amount[Fee].FeeAmount);
                        }
                    }
                }
            }
        }
        catch (err) {
            console.log(err);
        }

    };

    $scope.addFEECONFIG = function () {
        //$scope.FEECONFIG = {};

        $scope.FEECONFIG.AcademicYearId = $localStorage.FEECONFIG.AcademicYearId;
        $scope.FEECONFIG.ProgrammeInstancePartTermId = $localStorage.FEECONFIG.ProgrammeInstancePartTermId;
        $scope.FEECONFIG.FeeTypeId = $localStorage.FEECONFIG.FeeTypeId;
        //if ($scope.FEECONFIG.AcademicYearId === null || $scope.FEECONFIG.AcademicYearId === undefined
        //            || $scope.FEECONFIG.ProgramInstancePartTermId === null || $scope.FEECONFIG.ProgramInstancePartTermId === undefined
        //            || $scope.FEECONFIG.FeeTypeId === null || $scope.FEECONFIG.FeeTypeId === undefined
        //       ) {
        //            $mdDialog.show(
        //                $mdDialog.alert()
        //                    .parent(angular.element(document.querySelector('#popupContainer')))
        //                    .clickOutsideToClose(true)
        //                    .title("Error")
        //                     .textContent("Please complete the form before Click...")
        //                     .ariaLabel('Alert Dialog Demo')
        //                     .ok('Okay!')
        //             );
        //         }
        //        else {
        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/FeeConfigurationMultiAdd',
            data: $scope.FEECONFIG,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    alert(response.obj);
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
                    $scope.FEECONFIG = {};
                    $scope.getFEECONFIGList();
                    $scope.showtable = false;
                    $localStorage.FEECONFIG = {};
                    $state.go('FEECONFIGAdd');

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.cancelFEECONFIG = function () {
        $scope.FEECONFIG = {};
        $scope.modifyFEECONFIGFlag = false;
    };

    //EDIT FEE data code Start
    $scope.modifyFEECONFIGData = function (data) {
        //if (data.IsPublished == true) {
        //    data.IsPublished = true;
        //} else {
        //    data.IsPublished = false;
        //}
        $scope.FEECONFIG = data;
        $scope.getPRGList();
        $scope.getPBMList();
        $scope.getPIPList();
        $scope.getPIPTList();
        $scope.getFCList();
        $scope.modifyFEECONFIGFlag = true;
        $scope.showFormFlag = true;
    };
    $scope.EditMatrix = function (data) {
        $scope.FEECONFIG = {};
        $scope.FEECONFIG = data;
        if ($scope.FEECONFIG.IsPublished == true) {
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Fee Structure is Published you can not edit.")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );

        }
        else if ($scope.FEECONFIG.ProgrammeInstancePartTermId == null || $scope.FEECONFIG.ProgrammeInstancePartTermId === undefined
            || $scope.FEECONFIG.AcademicYearId == null || $scope.FEECONFIG.AcademicYearId === undefined
            || $scope.FEECONFIG.FeeTypeId == null || $scope.FEECONFIG.FeeTypeId === undefined
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
            $http({
                method: 'POST',
                url: 'api/FeeConfiguration/FeeCategoryfullistbyAYPTFT',
                data: $scope.FEECONFIG,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;
                    if (response.response_code == "0") {
                        $state.go('login');
                    }
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        $scope.FeeCategoryList = response.obj;//================================================================

                        var TotalAmountEdit = new Array();
                        let count1 = 0;
                        for (var FeeCategory in $scope.FeeCategoryList) {
                            var amt2Obj = {};
                            amt2Obj["Id"] = $scope.FeeCategoryList[FeeCategory].Id;
                            amt2Obj["FeeCategoryName"] = $scope.FeeCategoryList[FeeCategory].FeeCategoryName;
                            if ($scope.FeeCategoryList[FeeCategory].FeeCategoryPartTermId == 0) {
                                amt2Obj["Feecate"] = false;
                                amt2Obj["FeeState"] = true;
                            }
                            else {
                                amt2Obj["Feecate"] = true;
                                amt2Obj["FeeState"] = false;
                            }
                            amt2Obj["FeeCategoryPartTermId"] = $scope.FeeCategoryList[FeeCategory].FeeCategoryPartTermId;
                            amt2Obj["NoOfInstalment"] = $scope.FeeCategoryList[FeeCategory].NoOfInstalment;
                            amt2Obj["IsInstalmentGiven"] = $scope.FeeCategoryList[FeeCategory].IsInstalmentGiven;
                            TotalAmountEdit.push(amt2Obj);

                        }
                        $scope.showCategory = true;
                        $scope.TotalAmountEditList = TotalAmountEdit;

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };
    $scope.CheckFeeEdit = function () {

        for (var j in $scope.TotalAmountEditList) {

            if (Boolean($scope.TotalAmountEditList[j].Feecate) == true) {
                $scope.TotalAmountEditList[j].FeeState = false;
            }
            else {
                $scope.TotalAmountEditList[j].FeeState = true;
                $scope.TotalAmountEditList[j].Feecate = false;
                $scope.TotalAmountEditList[j].IsInstalmentGiven = false;
                $scope.TotalAmountEditList[j].NoOfInstalment = 0;

            }
        }
    };
    $scope.GetFSHListEdit = function () {
        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/FeeConfigurationGetByAYPTFT',
            data: $scope.FEECONFIG,
            headers: { "Content-Type": 'application/json' }
        })

            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    let count = 0;
                    let IsVerifyAll = false;
                    $scope.AmountDATAList = response.obj; //======================================================
                    for (var i in $scope.AmountDATAList) {
                        for (var k in $scope.AmountDATAList[i].Amount) {

                            $scope.AmountDATAList[i].Amount[k].FeeSHDeleteCheck = false;
                            count = count + 1;
                            if ($scope.AmountDATAList[i].Amount[k].IsPublished == true) {
                                IsPublishAll = true;
                            }
                            else {
                                IsPublishAll = false;
                            }
                        }
                    }
                    $scope.FEECONFIG.Count = count;
                    $scope.FEECONFIG.IsPublishAll = IsPublishAll;
                    $scope.MakeMatrixEdit();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };
    $scope.MakeMatrixEdit = function () {
        let count = 0;
        let countTotal = 0;
        let countDelete = 0;
        var TotalAmountEdit = new Array();
        var TotalAmountDelete = new Array();
        for (var i in $scope.TotalAmountEditList) {
            if ($scope.TotalAmountEditList[i].Feecate == true) {
                var amtEObj = {};
                amtEObj["TotalFee"] = 0;
                amtEObj["Id"] = $scope.TotalAmountEditList[i].Id;
                amtEObj["FeeCategoryName"] = $scope.TotalAmountEditList[i].FeeCategoryName;
                amtEObj["Feecate"] = $scope.TotalAmountEditList[i].Feecate;
                amtEObj["FeeCategoryPartTermId"] = $scope.TotalAmountEditList[i].FeeCategoryPartTermId;
                amtEObj["IsInstalmentGiven"] = $scope.TotalAmountEditList[i].IsInstalmentGiven;
                amtEObj["NoOfInstalment"] = $scope.TotalAmountEditList[i].NoOfInstalment;
                TotalAmountEdit.push(amtEObj);
                countTotal = countTotal + 1;
            }
            else {
                var amtDObj = {};
                amtDObj["Id"] = $scope.TotalAmountEditList[i].Id;
                amtDObj["FeeCategoryName"] = $scope.TotalAmountEditList[i].FeeCategoryName;
                amtDObj["Feecate"] = $scope.TotalAmountEditList[i].Feecate;
                amtDObj["FeeCategoryPartTermId"] = $scope.TotalAmountEditList[i].FeeCategoryPartTermId;
                TotalAmountDelete.push(amtDObj);
                countDelete = countDelete + 1;
            }
        }
        $scope.FEECONFIG.TotalAmountEditList = TotalAmountEdit;
        $scope.FEECONFIG.TotalAmountDeleteList = TotalAmountDelete;
        $scope.FEECONFIG.countDelete = countDelete;
        $scope.FEECONFIG.countTotal = countTotal;

        var arrData = new Array();
        for (var j in $scope.AmountDATAList) {
            var obj = {};
            var Amount = new Array();

            obj["Id"] = $scope.AmountDATAList[j].Id;
            obj["FeeSubHeadName"] = $scope.AmountDATAList[j].FeeSubHeadName;
            obj["FeeHeadId"] = $scope.AmountDATAList[j].FeeHeadId;
            obj["FeeHeadName"] = $scope.AmountDATAList[j].FeeHeadName;
            obj["AllowsNegativeValue"] = $scope.AmountDATAList[j].AllowsNegativeValue;
            for (var k in $scope.FEECONFIG.TotalAmountEditList) {
                var amtObj = {};
                if ($scope.FEECONFIG.TotalAmountEditList[k].FeeCategoryPartTermId == 0) {
                    amtObj["Id"] = $scope.FEECONFIG.TotalAmountEditList[k].Id;
                    amtObj["IsInstalmentAllowed"] = false;
                    amtObj["FeeAmount"] = 0;
                    amtObj["FeeCategoryName"] = $scope.FEECONFIG.TotalAmountEditList[k].FeeCategoryName;
                    amtObj["FeeConfigId"] = 0;
                    amtObj["IsPublished"] = false;
                    if ($scope.FEECONFIG.TotalAmountEditList[k].IsInstalmentGiven == true) {
                        amtObj["InstalmentGivenCheck"] = false;
                    }
                    else {
                        amtObj["InstalmentGivenCheck"] = true;
                    }
                }
                else {
                    for (var l in $scope.AmountDATAList[j].Amount) {
                        if ($scope.FEECONFIG.TotalAmountEditList[k].Id == $scope.AmountDATAList[j].Amount[l].Id) {
                            amtObj["Id"] = $scope.AmountDATAList[j].Amount[l].Id;
                            amtObj["IsInstalmentAllowed"] = $scope.AmountDATAList[j].Amount[l].IsInstalmentAllowed;
                            amtObj["FeeAmount"] = $scope.AmountDATAList[j].Amount[l].FeeAmount;
                            amtObj["FeeCategoryName"] = $scope.AmountDATAList[j].Amount[l].FeeCategoryName;
                            amtObj["FeeConfigId"] = $scope.AmountDATAList[j].Amount[l].FeeConfigId;
                            amtObj["IsPublished"] = $scope.AmountDATAList[j].Amount[l].IsPublished;
                            if ($scope.FEECONFIG.TotalAmountEditList[k].IsInstalmentGiven == true) {
                                amtObj["InstalmentGivenCheck"] = false;
                            }
                            else {
                                amtObj["InstalmentGivenCheck"] = true;
                            }
                        }
                    }
                }
                Amount.push(amtObj);
                count = count + 1;
            }
            obj["Amount"] = Amount;

            arrData.push(obj);
        }



        $scope.FEECONFIG.AmountEditList = arrData;
        $scope.FEECONFIG.Count = count;

        $scope.showtable = true;

    };
    $scope.TotalFeeEdit = function () {

        try {
            for (var TotalAmountEdit in $scope.FEECONFIG.TotalAmountEditList) {
                $scope.FEECONFIG.TotalAmountEditList[TotalAmountEdit].TotalFee = 0;
            }
            for (var i in $scope.FEECONFIG.AmountEditList) {
                for (var k in $scope.FEECONFIG.AmountEditList[i].Amount) {
                    for (var TotalAmountEdit in $scope.FEECONFIG.TotalAmountEditList) {
                        if ($scope.FEECONFIG.TotalAmountEditList[TotalAmountEdit].Id === $scope.FEECONFIG.AmountEditList[i].Amount[k].Id) {
                            $scope.FEECONFIG.TotalAmountEditList[TotalAmountEdit].TotalFee = parseFloat($scope.FEECONFIG.TotalAmountEditList[TotalAmountEdit].TotalFee) + parseFloat($scope.FEECONFIG.AmountEditList[i].Amount[k].FeeAmount);
                        }

                    }
                }
            }
        }
        catch (err) {
            console.log(err);
        }



    };
    $scope.modifyFEECONFIG = function () {
        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/FeeConfigurationMulti1Edit',
            data: $scope.FEECONFIG,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.FEECONFIG = {};
                    $scope.getFEECONFIGList();
                    $scope.modifyFEECONFIGFlag = false;
                    $scope.showFormFlag = false;
                    $scope.showCategory = false;
                    $scope.showtable = false;
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
        //   }
    };
    //EDIT FEE data code end

    //Publish data code Start
    $scope.PublishData = function (FCPT) {
        $scope.newFee = {};
        $scope.newFee.AcademicYearId = FCPT.AcademicYearId;
        $scope.newFee.ProgrammeInstancePartTermId = FCPT.ProgrammeInstancePartTermId;
        $scope.newFee.FeeTypeId = FCPT.FeeTypeId;
        $scope.newFee.Id = FCPT.Id
        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/FeeCategoryPartTermMapIsPublishedEnable',
            data: $scope.newFee,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.getFEECONFIGList();
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
    };
    $scope.Unpublish = function (FCPT) {
        //$scope.FEECONFIG = {};
        $localStorage.printdataget = FCPT;
        $state.go('FeeUnpublish');
    };
    $scope.UnPublishData = function (ev) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to UnPublish?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.newFee = {};
            $scope.newFee.AcademicYearId = $localStorage.printdataget.AcademicYearId;
            $scope.newFee.ProgrammeInstancePartTermId = $localStorage.printdataget.ProgrammeInstancePartTermId;
            $scope.newFee.FeeTypeId = $localStorage.printdataget.FeeTypeId;
            $scope.newFee.Id = $localStorage.printdataget.Id
            $http({
                method: 'POST',
                url: 'api/FeeConfiguration/FeeCategoryPartTermMapIsPublishedDisable',
                data: $scope.newFee,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;
                    if (response.response_code == "0") {
                        $state.go('login');
                    }
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
                        $state.go('FEECONFIGUnPublish');
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }, function () {
            $scope.status = 'You decided to keep your debt.';
        });
    };
    $scope.FeePaidStudentlistbyAYPTFT = function () {

        $scope.newFee = {};
        $scope.newFee = $localStorage.printdataget;
        console.log($scope.newFee.FeeTypeName + "--Admission Fee");
        if ($scope.newFee.FeeTypeName == "Admission Fee") {

            $http({
                method: 'POST',
                url: 'api/FeeConfiguration/FeePaidStudentlistbyAYPTFT',
                data: $scope.newFee,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');
                    }
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        $scope.StudentData = response.obj;
                        $(document).ready(function () {
                            $('#StudentDetail').DataTable({
                                //"bPaginate": false,
                                "ordering": false,
                                dom: 'Bfrtip',
                                buttons: [
                                    { extend: 'csv', title: 'List Of Student Paid fees.', exportOptions: { columns: "thead th:not(.noExport)" } }
                                    ,
                                    { extend: 'excel', title: 'List Of Student Paid fees.', exportOptions: { columns: "thead th:not(.noExport)" } }
                                    , { extend: 'pdf', title: 'List Of Student Paid fees.', exportOptions: { columns: "thead th:not(.noExport)" } }
                                    , { extend: 'print', title: 'List Of Student Paid fees.', exportOptions: { columns: "thead th:not(.noExport)" } }

                                ]
                            });
                        });

                        
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
        else {
            $http({
                method: 'POST',
                url: 'api/FeeConfiguration/FeePaidApplicantlistbyAYPTFT',
                data: $scope.newFee,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');
                    }
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        $scope.ApplicantData = response.obj;
                        $(document).ready(function () {
                            $('#ApplicantDetail').DataTable({
                                //"bPaginate": false,
                                "ordering": false,
                                dom: 'Bfrtip',
                                buttons: [
                                    { extend: 'csv', title: 'List Of Applicant Paid fees.', exportOptions: { columns: "thead th:not(.noExport)" } }
                                    ,
                                    { extend: 'excel', title: 'List Of Applicant Paid fees.', exportOptions: { columns: "thead th:not(.noExport)" } }
                                    , { extend: 'pdf', title: 'List Of Applicant Paid fees.', exportOptions: { columns: "thead th:not(.noExport)" } }
                                    , { extend: 'print', title: 'List Of Applicant Paid fees.', exportOptions: { columns: "thead th:not(.noExport)" } }

                                ]
                            });
                        });
                        
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    //Publish data code end

    //Verify data code Start
    $scope.getFEECONFIGFilteredList = function () {

        var data = new Object();
        $scope.FEECONFIGFilteredTableParams = new NgTableParams({
        }, {
            dataset: data
        });

        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/getFEECONFIGFilteredList',
            data: $scope.FeeVerifyData,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    if (response.obj == "No Record Found") {
                        $scope.Showbuttonflag = false;
                    }
                }
                else {
                    $scope.showtable = false;
                    $scope.FEECONFIGFilteredTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    $scope.VerifyGetData = function (FCPT) {
        //$scope.FEECONFIG = {};
        $scope.VerifyMatrix(FCPT);
    };
    $scope.Verifyprint = function (FCPT) {
        //$scope.FEECONFIG = {};
        $localStorage.printdataget = FCPT;
        $state.go('FeePrintdata');
    };

    $scope.VerifyMatrix = function (data) {
        $scope.FEECONFIG = {};
        $scope.FEECONFIG = data;
        if ($scope.FEECONFIG.ProgrammeInstancePartTermId == null || $scope.FEECONFIG.ProgrammeInstancePartTermId === undefined
            || $scope.FEECONFIG.AcademicYearId == null || $scope.FEECONFIG.AcademicYearId === undefined
            || $scope.FEECONFIG.FeeTypeId == null || $scope.FEECONFIG.FeeTypeId === undefined
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

            $http({
                method: 'POST',
                url: 'api/FeeConfiguration/FeeCategorylistbyAYPTFT',
                data: $scope.FEECONFIG,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;
                    if (response.response_code == "0") {
                        $state.go('login');
                    }
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {

                        $scope.FeeCategoryList = response.obj;//================================================================
                        let IsVerifyAll = false;
                        var TotalAmountEdit = new Array();
                        for (var FeeCategory in $scope.FeeCategoryList) {
                            if ($scope.FeeCategoryList[FeeCategory].IsVerified == true) {
                                IsVerifyAll = true;
                            }
                            else {
                                IsVerifyAll = false;
                            }
                            var amt2Obj = {};
                            amt2Obj["TotalFee"] = 0;
                            amt2Obj["Id"] = $scope.FeeCategoryList[FeeCategory].Id;
                            amt2Obj["FeeCategoryPartTermId"] = $scope.FeeCategoryList[FeeCategory].FeeCategoryPartTermId;
                            TotalAmountEdit.push(amt2Obj);
                        }
                        $scope.FEECONFIG.IsVerifyAll = IsVerifyAll;
                        $scope.FEECONFIG.TotalAmountEditList = TotalAmountEdit;
                        $http({
                            method: 'POST',
                            url: 'api/FeeConfiguration/FeeConfigurationGetByAYPTFT',
                            data: $scope.FEECONFIG,
                            headers: { "Content-Type": 'application/json' }
                        })

                            .success(function (response) {
                                $rootScope.showLoading = false;
                                if (response.response_code != "200") {
                                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                                }
                                else {
                                    let count = 0;

                                    $scope.AmountDATAList = response.obj; //======================================================

                                    $scope.FEECONFIG.Count = count;

                                    $scope.showtable = true;
                                    //$state.go('FEECONFIGEdit');
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
        }
    };
    $scope.PrintMatrix = function () {
        $scope.FEECONFIG = {};
        $scope.FEECONFIG = $localStorage.printdataget;
        if ($scope.FEECONFIG.FeeTypeName == "Admission Fee") {
            $scope.FEECONFIG.IsAdmissionFee = true;
        }
        if ($scope.FEECONFIG.ProgrammeInstancePartTermId == null || $scope.FEECONFIG.ProgrammeInstancePartTermId === undefined
            || $scope.FEECONFIG.AcademicYearId == null || $scope.FEECONFIG.AcademicYearId === undefined
            || $scope.FEECONFIG.FeeTypeId == null || $scope.FEECONFIG.FeeTypeId === undefined
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

            $http({
                method: 'POST',
                url: 'api/FeeConfiguration/FeeCategorylistbyAYPTFT',
                data: $scope.FEECONFIG,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;
                    if (response.response_code == "0") {
                        $state.go('login');
                    }
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {

                        $scope.FeeCategoryList = response.obj;//================================================================
                        var TotalAmountEdit = new Array();
                        for (var FeeCategory in $scope.FeeCategoryList) {
                            var amt2Obj = {};
                            amt2Obj["TotalFee"] = 0;
                            amt2Obj["Id"] = $scope.FeeCategoryList[FeeCategory].Id;
                            amt2Obj["FeeCategoryName"] = $scope.FeeCategoryList[FeeCategory].FeeCategoryName;
                            amt2Obj["FeeCategoryPartTermId"] = $scope.FeeCategoryList[FeeCategory].FeeCategoryPartTermId;
                            amt2Obj["IsInstalmentGiven"] = $scope.FeeCategoryList[FeeCategory].IsInstalmentGiven;
                            TotalAmountEdit.push(amt2Obj);
                        }
                        $scope.FEECONFIG.TotalAmountEditList = TotalAmountEdit;
                        $http({
                            method: 'POST',
                            url: 'api/FeeConfiguration/FeeConfigurationGetByAYPTFT',
                            data: $scope.FEECONFIG,
                            headers: { "Content-Type": 'application/json' }
                        })

                            .success(function (response) {
                                $rootScope.showLoading = false;
                                if (response.response_code != "200") {
                                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                                }
                                else {

                                    $scope.AmountDATAList = response.obj; //======================================================
                                    for (var TotalAmountEdit in $scope.FEECONFIG.TotalAmountEditList) {
                                        $scope.FEECONFIG.TotalAmountEditList[TotalAmountEdit].TotalFee = 0;
                                    }
                                    for (var i in $scope.AmountDATAList) {
                                        for (var k in $scope.AmountDATAList[i].Amount) {
                                            for (var TotalAmountEdit in $scope.FEECONFIG.TotalAmountEditList) {
                                                if ($scope.FEECONFIG.TotalAmountEditList[TotalAmountEdit].Id === $scope.AmountDATAList[i].Amount[k].Id) {
                                                    $scope.FEECONFIG.TotalAmountEditList[TotalAmountEdit].TotalFee = parseFloat($scope.FEECONFIG.TotalAmountEditList[TotalAmountEdit].TotalFee) + parseFloat($scope.AmountDATAList[i].Amount[k].FeeAmount);

                                                }

                                            }
                                        }
                                    }
                                }
                                console.log($scope.FEECONFIG);
                                $(document).ready(function () {
                                    $('#example').DataTable({
                                        "bPaginate": false,
                                        "ordering": false,
                                        dom: 'Bfrtip',
                                        buttons: [
                                            { extend: 'csv', title: 'Fee Configuration Verify for ' + $scope.FEECONFIG.FacultyName + ':: ' + $scope.FEECONFIG.ProgrammeName + ':: ' + $scope.FEECONFIG.BranchName + ':: ' + $scope.FEECONFIG.FeeTypeName + ' Detail ', exportOptions: { columns: "thead th:not(.noExport)" } }
                                            ,
                                            { extend: 'excel', title: 'Fee Configuration Verify for ' + $scope.FEECONFIG.FacultyName + ':: ' + $scope.FEECONFIG.ProgrammeName + ':: ' + $scope.FEECONFIG.BranchName + ':: ' + $scope.FEECONFIG.FeeTypeName + ' Detail ', exportOptions: { columns: "thead th:not(.noExport)" } }
                                            , { extend: 'pdf', orientation: 'landscape', title: 'Fee Configuration Verify for ' + $scope.FEECONFIG.FacultyName + ':: ' + $scope.FEECONFIG.ProgrammeName + ':: ' + $scope.FEECONFIG.BranchName + ':: ' + $scope.FEECONFIG.FeeTypeName + ' Detail ', exportOptions: { columns: "thead th:not(.noExport)" } }
                                            , { extend: 'print', title: 'Fee Configuration Verify for ' + $scope.FEECONFIG.FacultyName + ':: ' + $scope.FEECONFIG.ProgrammeName + ':: ' + $scope.FEECONFIG.BranchName + ':: ' + $scope.FEECONFIG.FeeTypeName + ' Detail ', exportOptions: { columns: "thead th:not(.noExport)" } }

                                        ]
                                    });
                                });
                                
                            })
                            .error(function (res) {
                                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                            });
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.TotalFeeVerify = function () {

        try {
            for (var TotalAmountEdit in $scope.FEECONFIG.TotalAmountEditList) {
                $scope.FEECONFIG.TotalAmountEditList[TotalAmountEdit].TotalFee = 0;
            }
            for (var i in $scope.AmountDATAList) {
                for (var k in $scope.AmountDATAList[i].Amount) {
                    for (var TotalAmountEdit in $scope.FEECONFIG.TotalAmountEditList) {
                        if ($scope.FEECONFIG.TotalAmountEditList[TotalAmountEdit].Id === $scope.AmountDATAList[i].Amount[k].Id) {
                            $scope.FEECONFIG.TotalAmountEditList[TotalAmountEdit].TotalFee = parseFloat($scope.FEECONFIG.TotalAmountEditList[TotalAmountEdit].TotalFee) + parseFloat($scope.AmountDATAList[i].Amount[k].FeeAmount);

                        }

                    }
                }
            }
        }
        catch (err) {
            console.log(err);
        }
    };
    $scope.VerifyFEECONFIG = function (data) {
        $scope.FEECONFIG.AmountList = $scope.AmountDATAList;

        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/FeeConfigurationIsVerifiedEnable',
            data: $scope.FEECONFIG,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    //$scope.Showbuttonflag = false;
                    //$scope.FeeVerifyData = {};
                    $scope.getFEECONFIGFilteredList();
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

    };
    $scope.UnVerifyFEECONFIG = function (data) {

        $scope.FEECONFIG.AmountList = $scope.AmountDATAList;


        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/FeeConfigurationIsVerifiedDisable',
            data: $scope.FEECONFIG,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.getFEECONFIGFilteredList();
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

    };

    $scope.showVERIFYbutton = function () {

        $scope.getFEECONFIGFilteredList();
        for (var i in $scope.AcademicList) {
            if ($scope.AcademicList[i].Id == $scope.FeeVerifyData.AcademicYearId) {
                $scope.FeeVerifyData.AcademicYearCode = $scope.AcademicList[i].AcademicYearCode;
            }
        }
        for (var j in $scope.FacultyList) {
            if ($scope.FacultyList[j].Id == $scope.FeeVerifyData.FacultyId) {
                $scope.FeeVerifyData.FacultyName = $scope.FacultyList[j].FacultyName;
            }
        }
        for (var k in $scope.FTList) {
            if ($scope.FTList[k].Id == $scope.FeeVerifyData.FeeTypeId) {
                $scope.FeeVerifyData.FeeTypeName = $scope.FTList[k].FeeTypeName;
            }
        }
        $scope.Showbuttonflag = true;
    };
    $scope.VerifyallFee = function () {
        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/FeeConfigurationIsVerifyallEnable',
            data: $scope.FeeVerifyData,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.showtable = false;
                    $scope.Showbuttonflag = false;
                    $scope.FeeVerifyData = {};
                    $scope.getFEECONFIGList();
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
    }
    //Verify data code end

    $scope.newFEECONFIGAdd = function () {
        $state.go('FEECONFIGAdd');
    };
    $scope.backToList = function () {
        $state.go('FEECONFIGEdit');
    };
    $scope.displayFEECONFIG = function (data) {
        $scope.FEECONFIG = data;
    };


    // Copy Fee To Other Part Term Code Start Here
    /*List of Fee Configured Programme Instance Part Term */
    $scope.GetFeeConfiguredPIPT = function () {
        $scope.PIPTList = {};
        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/PIPTget',
            data: $scope.FeeCopy,
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
                        $scope.PIPTList = {};
                    }
                }
                else {
                    $scope.PIPTList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    /*List of Fee Not Configured Programme Instance Part Term */
    $scope.GetFeenotConfiguredPIPT = function () {
        $scope.PIPTfncList = {};
        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/FeeGetIncPT',
            data: $scope.FeeCopy,
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
                        $scope.PIPTfncList = {};
                    }
                }
                else {
                    $scope.PIPTfncList = response.obj;
                    $scope.PIPTtableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });

                    $scope.ShowPIPTFlag = true;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    /*List of Fee Type By Programme Instance Part Term */
    $scope.GetFTbyPIPT = function () {
        $scope.FCFTList = {};

        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/FeeGetFTByPT',
            data: $scope.FeeCopy,
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
                        $scope.FCFTList = {};
                    }
                }
                else {
                    $scope.FCFTList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    /*List of Fee Category By Programme Instance Part Term and FeeType */
    $scope.GetFCbyPIPTFT = function () {
        $scope.FeeCopyFCList = {};

        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/FeeGetFCbyPTFT',
            data: $scope.FeeCopy,
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
                        $scope.FeeCopyFCList = {};
                    }
                }
                else {
                    $scope.FeeCopyFCList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    $scope.FeeCopycall = function () {

        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/FeeCopy',
            data: $scope.FeeCopy,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    alert(response.obj);
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
                    $scope.FeeCopy = {};
                    $scope.ShowPIPTFlag = false;
                    $scope.FeeCopyFCList = {};
                    $scope.FCFTList = {};
                    $scope.PIPTList = {};
                    $scope.IncAcademicYearListGet();

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };
    // Copy Fee To Other Part Term Code End Here   




});
