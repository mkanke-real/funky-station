// SPDX-FileCopyrightText: 2022 Leon Friedrich <60421075+ElectroJr@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 Fildrance <fildrance@gmail.com>
// SPDX-FileCopyrightText: 2024 LordCarve <27449516+LordCarve@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 Nemanja <98561806+EmoGarbage404@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 Piras314 <p1r4s@proton.me>
// SPDX-FileCopyrightText: 2024 Tadeo <td12233a@gmail.com>
// SPDX-FileCopyrightText: 2024 username <113782077+whateverusername0@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 whateverusername0 <whateveremail>
// SPDX-FileCopyrightText: 2025 Tay <td12233a@gmail.com>
// SPDX-FileCopyrightText: 2025 misghast <51974455+misterghast@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 slarticodefast <161409025+slarticodefast@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 taydeo <td12233a@gmail.com>
//
// SPDX-License-Identifier: MIT

using System.Diagnostics.CodeAnalysis;
using Content.Shared.Mind;
using Content.Shared.Store;
using Content.Shared.Store.Components;
using Robust.Shared.Prototypes;

namespace Content.Server.Store.Systems;

public sealed partial class StoreSystem
{
    /// <summary>
    /// Refreshes all listings on a store.
    /// Do not use if you don't know what you're doing.
    /// </summary>
    /// <param name="component">The store to refresh</param>
    public void RefreshAllListings(StoreComponent component)
    {
        component.Listings = GetAllListings();
        _storeDiscount.ApplyDiscounts(component.Listings, component);
    }

    /// <summary>
    /// Gets all listings from a prototype.
    /// </summary>
    /// <returns>All the listings</returns>
    public HashSet<ListingData> GetAllListings()
    {
        var allListings = _proto.EnumeratePrototypes<ListingPrototype>();

        var allData = new HashSet<ListingData>();

        foreach (var listing in allListings)
        {
            allData.Add((ListingData) listing.Clone());
        }

        return allData;
    }

    /// <summary>
    /// Adds a listing from an Id to a store
    /// </summary>
    /// <param name="component">The store to add the listing to</param>
    /// <param name="listingId">The id of the listing</param>
    /// <returns>Whetehr or not the listing was added successfully</returns>
    public bool TryAddListing(StoreComponent component, string listingId)
    {
        if (!_proto.TryIndex<ListingPrototype>(listingId, out var proto))
        {
            Log.Error("Attempted to add invalid listing.");
            return false;
        }
        return TryAddListing(component, proto);
    }

    /// <summary>
    /// Adds a listing to a store
    /// </summary>
    /// <param name="component">The store to add the listing to</param>
    /// <param name="listing">The listing</param>
    /// <returns>Whether or not the listing was add successfully</returns>
    public bool TryAddListing(StoreComponent component, ListingData listing)
    {
        return component.Listings.Add(listing);
    }

    /// <summary>
    /// Gets the available listings for a store
    /// </summary>
    /// <param name="buyer">Either the account owner, user, or an inanimate object (e.g., surplus bundle)</param>
    /// <param name="store"></param>
    /// <param name="component">The store the listings are coming from.</param>
    /// <returns>The available listings.</returns>
    public IEnumerable<ListingData> GetAvailableListings(EntityUid buyer, EntityUid store, StoreComponent component)
    {
        return GetAvailableListings(buyer, component.Listings, component.Categories, store);
    }

    /// <summary>
    /// Gets the available listings for a user given an overall set of listings and categories to filter by.
    /// </summary>
    /// <param name="buyer">Either the account owner, user, or an inanimate object (e.g., surplus bundle)</param>
    /// <param name="listings">All of the listings that are available. If null, will just get all listings from the prototypes.</param>
    /// <param name="categories">What categories to filter by.</param>
    /// <param name="storeEntity">The physial entity of the store. Can be null.</param>
    /// <returns>The available listings.</returns>
    public IEnumerable<ListingData> GetAvailableListings(
        EntityUid buyer,
        HashSet<ListingData>? listings,
        HashSet<ProtoId<StoreCategoryPrototype>> categories,
        EntityUid? storeEntity = null)
    {
        listings ??= GetAllListings();

        foreach (var listing in listings)
        {
            if (!ListingHasCategory(listing, categories))
                continue;

            if (listing.Conditions != null)
            {
                var args = new ListingConditionArgs(GetBuyerMind(buyer), storeEntity, listing, EntityManager);
                var conditionsMet = true;

                foreach (var condition in listing.Conditions)
                {
                    if (!condition.Condition(args))
                    {
                        conditionsMet = false;
                        break;
                    }
                }

                if (!conditionsMet)
                    continue;
            }

            yield return listing;
        }
    }

    /// <summary>
    /// Returns the entity's mind entity, if it has one, to be used for listing conditions.
    /// If it doesn't have one, or is a mind entity already, it returns itself.
    /// </summary>
    /// <param name="buyer">The buying entity.</param>
    public EntityUid GetBuyerMind(EntityUid buyer)
    {
        if (!HasComp<MindComponent>(buyer) && _mind.TryGetMind(buyer, out var buyerMind, out var _))
            return buyerMind;

        return buyer;
    }

    /// <summary>
    /// Checks if a listing appears in a list of given categories
    /// </summary>
    /// <param name="listing">The listing itself.</param>
    /// <param name="categories">The categories to check through.</param>
    /// <returns>If the listing was present in one of the categories.</returns>
    public bool ListingHasCategory(ListingData listing, HashSet<ProtoId<StoreCategoryPrototype>> categories)
    {
        foreach (var cat in categories)
        {
            if (listing.Categories.Contains(cat))
                return true;
        }
        return false;
    }
}
