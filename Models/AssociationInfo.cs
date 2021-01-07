using Microsoft.Xrm.Sdk.Metadata;

namespace PowerPortalWebAPIHelper
{
    public class AssociationInfo
    {
        public AssociationInfo(ManyToManyRelationshipMetadata mToMRelationsip)
        {
            MToMRelationsip = mToMRelationsip;
        }

        public AssociationInfo(OneToManyRelationshipMetadata mToOneRelationsip)
        {
            MToOneRelationsip = mToOneRelationsip;
        }

        public ManyToManyRelationshipMetadata MToMRelationsip { get; }
        public OneToManyRelationshipMetadata MToOneRelationsip { get; }

        public override string ToString()
        {
            if (MToMRelationsip != null)
            {
                return MToMRelationsip.SchemaName;
            }
            else if (MToOneRelationsip != null)
            {
                return MToOneRelationsip.SchemaName;
            }
            else
            {
                return base.ToString();
            }
            
        }
    }
}