CREATE PROCEDURE [dbo].[GetActivityAdsTags]
	@ActivityId int
AS
	select * from Ads
where deleted=0 and Id in (
    select distinct TM.AdvertisemnetID from 
	(		
		select * from Ads_TagValue S
		where S.AdvertisemnetID not in(
				select T.AdvertisemnetID from Ads_TagValue T
				where  T.TagValueID=any(
					(select TagValueID from Ads_TagValue T1 where T1.AdvertisemnetID=T.AdvertisemnetID) 
						except 
					(select TagValueID from Activity_TagValue where ActivityId=@ActivityId)
				)
		) 
	) TM
	where TM.AdvertisemnetID not in(
		select S.AdvertisemnetID from 
		( 
			select * from (
				select * from Ads_TagValue S
				where S.AdvertisemnetID not in(
					select T.AdvertisemnetID from Ads_TagValue T
					where  T.TagValueID=any(
						(select TagValueID from Ads_TagValue T1 where T1.AdvertisemnetID=T.AdvertisemnetID) 
							except 
						(select TagValueID from Activity_TagValue where ActivityId=@ActivityId)
					)
				)
			 ) S1
			 where exists(
				(select TagValueID from Activity_TagValue where ActivityId=@ActivityId)
					except
				select TagValueID from 
					(	
						select * from Ads_TagValue S
						where S.AdvertisemnetID not in(
							select T.AdvertisemnetID from Ads_TagValue T
							where  T.TagValueID=any(
								(select TagValueID from Ads_TagValue T1 where T1.AdvertisemnetID=T.AdvertisemnetID) 
									except 
								(select TagValueID from Activity_TagValue where ActivityId=@ActivityId)
							)
						) 
				) S2 where S2.AdvertisemnetID=S1.AdvertisemnetID
			)
  
		) S
	)
	union
			
	select distinct S1.AdvertisemnetID from 
	(
		select * from Ads_TagValue S
		where S.AdvertisemnetID in (
			select T.AdvertisemnetID from Ads_TagValue T
			where  T.TagValueID=any(
				(select TagValueID from Ads_TagValue T1 where T1.AdvertisemnetID=T.AdvertisemnetID) 
					except 
				(select TagValueID from Activity_TagValue where ActivityId=@ActivityId)
			)
		)
		
	) S1
	where not exists
	(
		(select TagValueID from Activity_TagValue where ActivityId=@ActivityId)
			except
		select TagValueID from 
		(
			select * from Ads_TagValue S
			where S.AdvertisemnetID in (
				select T.AdvertisemnetID from Ads_TagValue T
				where  T.TagValueID=any(
					(select TagValueID from Ads_TagValue T1 where T1.AdvertisemnetID=T.AdvertisemnetID) 
						except 
					(select TagValueID from Activity_TagValue where ActivityId=@ActivityId)
				)
			)	
		) S2 where S2.AdvertisemnetID=S1.AdvertisemnetID
	) and exists (select TagValueID from Activity_TagValue where ActivityId=@ActivityId)		
)