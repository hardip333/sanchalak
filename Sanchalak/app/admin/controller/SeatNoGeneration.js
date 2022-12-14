app.controller('SeatNoGenerationCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams, $window) {

    $rootScope.pageTitle = "Manage Seat Number Generation";


    $rootScope.showLoading = false;

    $scope.cancelSeatNoGeneration = function () {
        $scope.examseat = {
     
            ProgrammeId:0,
            ExamMasterId: 0,
            FacultyExamMapId: 0,
            ProgrammePartTermId: 0,
            BranchId:0
        };
    };

    $scope.cancelSeatNoGeneration();


    $scope.ReloadSeatNoGeneration = function () {
    $scope.SeatNoGeneration = [
        {
            DisplayName: "Institute Wise",
            FieldName: "Institute Wise",
                IsSelected: "",
                SequenceNO: "",
            OrderingType: "",
        }, {
            DisplayName: "Center Wise",
            FieldName: "Center Wise",
            IsSelected: "",
            SequenceNO: "",
            OrderingType: "",
        }, {
            DisplayName: "Paper Wise",
            FieldName: "Paper Wise",
            IsSelected: "",
            SequenceNO: "",
            OrderingType: "",
       
        }, {
            DisplayName: "Full Name Wise",
            FieldName: "Full Name Wise",
            IsSelected: "",
            SequenceNO: "",
            OrderingType: "",

        }, {
            DisplayName: "First Name Wise",
            FieldName: "First Name Wise",
            IsSelected: "",
            SequenceNO: "",
            OrderingType: "",

        }, {
            DisplayName: "Last Name Wise",
            FieldName: "Last Name Wise",
            IsSelected: "",
            SequenceNO: "",
            OrderingType: "",

        }, {
            DisplayName: "Appearance Type Wise",
            FieldName: "Appearance Type  Wise",
            IsSelected: "",
            SequenceNO: "",
            OrderingType: "",

        },

    ]
    };
    // send static array to backend side
    //$scope.SeatNoGeneration = [
    //    {
    //        DisplayName: "Institute Wise",
    //        FieldName: "Institute Wise",
    //        IsSelected : "",
    //        SequenceNO: "" ,
    //        OrderingType: "",
    //    }, {
    //        DisplayName: "Center Wise",
    //        FieldName: "Center Wise",
    //        IsSelected: "",
    //        SequenceNO: "",
    //        OrderingType: "",
    //    }, {
    //        DisplayName: "Paper Wise",
    //        FieldName: "Paper Wise",
    //        IsSelected: "",
    //        SequenceNO: "",
    //        OrderingType: "",
       
    //    }, {
    //        DisplayName: "Full Name Wise",
    //        FieldName: "Full Name Wise",
    //        IsSelected: "",
    //        SequenceNO: "",
    //        OrderingType: "",

    //    }, {
    //        DisplayName: "First Name Wise",
    //        FieldName: "First Name Wise",
    //        IsSelected: "",
    //        SequenceNO: "",
    //        OrderingType: "",

    //    }, {
    //        DisplayName: "Last Name Wise",
    //        FieldName: "Last Name Wise",
    //        IsSelected: "",
    //        SequenceNO: "",
    //        OrderingType: "",

    //    }, {
    //        DisplayName: "Appearance Type Wise",
    //        FieldName: "Appearance Type  Wise",
    //        IsSelected: "",
    //        SequenceNO: "",
    //        OrderingType: "",

    //    },

    //]

    $scope.ReloadSeatNoGeneration();
    // for generate saet no 
    $scope.generateGenerateSeatNo = function (data) {

        $rootScope.showLoading = true;
   
        var xml = new Object();
        xml.SpecialisationId = data.BranchId;
        xml.ProgrammePartTermId = data.ProgrammePartTermId;
        xml.SeatNoGeneration = $scope.SeatNoGeneration;
        xml.ExamMasterId = data.ExamMasterId;
        xml.IncludeFresher = data.IncludeFresher;
        xml.IncludeRepeater = data.IncludeRepeater;
        xml.IncludeNeverAppeared = data.IncludeNeverAppeared;


        $http({
            method: 'POST',
            url: 'api/ExamFormMaster/GenerateSeatNo',
            data: xml,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
/*                    $scope.GenerateSeatNoList = response.obj;*/
                   alert(response.obj);
                    $scope.examseat = {};
                    $scope.info = {};
                    $scope.ReloadSeatNoGeneration();
                 ///*   $scope.seatno = response.obj.SeatNoGeneration;*/

                 //   $scope.seatgenerationTableParams = new NgTableParams({
                 //       count: 1000
                 //   }, {
                 //       dataset: $scope.seatno
                 //   });
                }
                /*          $scope.TimeTableMasterGetPendingList*/
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

  
    // for details of no of exam forms 
    $scope.getExamFormDetails = function (data) {
        $rootScope.showLoading = true;

        var xml = new Object();
        
        xml.BranchId = data.BranchId;
        xml.ExamMasterId = data.ExamMasterId;
        xml.ProgrammePartTermId = data.ProgrammePartTermId;
 /*       if (data.BranchId !== null && data.ProgrammePartTermId !== null) {*/

            $http({
                method: 'POST',
                url: 'api/ExamFormMaster/ExamFormDetails',
                data: xml,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        alert(response.obj);
                    }
                    else {
                        $scope.info = response.obj;

                    }
                })
                .error(function (res) {

                    $rootScope.showLoading = false;
                    alert(res.obj);
                });
       /* }*/
    };
 /*   $scope.getMstExamCenterGet();*/




   

  

});