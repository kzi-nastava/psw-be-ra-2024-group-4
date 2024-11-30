INSERT INTO encounter."Encounters"(
    "Id", "Title", "Description", "Longitude", "Latitude", "XP", "Status", "Type", 
    "SocialData_RequiredParticipants", "SocialData_Radius", 
    "HiddenLocationData_ImageUrl", "HiddenLocationData_ActivationRadius", 
    "MiscData_ActionDescription", "Instances", "IsRequired")
    VALUES
    (-1, 'Mystic Forest Gathering', 
     'A mysterious gathering is taking place deep within the enchanted forest. Only the brave can join.', 
     45.45, 45.45, 5, 0, 0, 
     5, 1500, NULL, NULL, NULL, '[]',True),     
    (-2, 'Legends of the Lost Village', 
     'Gather a team to uncover the secrets of the abandoned village. Teamwork is essential.', 
     45.45, 45.45, 10, 0, 0, 
     5, 1500, NULL, NULL, NULL, '[{{"UserId": -21, "Status": 0, "CompletionTime": "2024-11-25T12:00:00+00"}},
                                 {{"UserId": -22, "Status": 0, "CompletionTime": "2024-11-25T12:00:00+00"}},
                                 {{"UserId": -23, "Status": 0, "CompletionTime": "2024-11-25T12:00:00+00"}},
                                 {{"UserId": -24, "Status": 0, "CompletionTime": "2024-11-25T12:00:00+00"}},
                                 {{"UserId": -25, "Status": 0, "CompletionTime": "2024-11-25T12:00:00+00"}}]',True),
    (-3, 'NewEncounter', 
         'Encounter wonderful description', 
         10.0, 45.0, 5, 0, 0, 
         5, 1500, NULL, NULL, NULL, '[]', True);
